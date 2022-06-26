import { AfterViewChecked, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { UserStorageService } from '../../services/user-storage.service';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { Chat } from 'src/app/models/Chat/Chat';
import { ChatService } from '../../services/chat.service';
import { ActivatedRoute, Params } from '@angular/router';
import { map, switchMap } from 'rxjs/operators';
import { Author } from 'src/app/models/Author/Author';
import { ImageService } from '../../services/image.service';
import { Guid } from 'guid-typescript';
import { Message } from 'src/app/models/Message/Message';
import { SignalrService } from '../../services/signalr.service';
import { User } from 'src/app/models/User/User';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit, AfterViewChecked {
  @ViewChild('newMessageBody') private _messageBody!: ElementRef;
  @ViewChild('content') private _content!: ElementRef;


  chats?: Chat[];
  chatId: string | null = null;
  currentChat?: Chat;
  membersExceptYou?: Author[];
  connection?: HubConnection;

  constructor(public _userStorage: UserStorageService,
    private _route: ActivatedRoute,
    private _chatService: ChatService,
    private _userService: UserService,
    private _imageService: ImageService,
    private _signalrService: SignalrService) { }

  ngOnInit(): void {
    this._route.data.pipe(
      map<any, User>((data: { user: User }) => data.user)
    ).subscribe(res => {
      this._userService.saveUserData(res);
    });

    this._signalrService.ConfigureService();
    this.connection = this._signalrService.GetHubConnection();
    this.connection?.start();

    this.chatId = this._route.snapshot.queryParams['chatId'];

    if (this.chatId) {
      this.openChat(this.chatId);
    }


    this._chatService.getChatsByUser(this._userStorage.user?.id!).subscribe(res => {
      res.map(_ => _.members.map(a => a.avatarUrl = this._imageService.fromBase64ToUrl(a.avatar)));
      res.map(_ => _.messages.map(a => a.author.avatarUrl = this._imageService.fromBase64ToUrl(a.author.avatar)));
      this.chats = res;
    });

    this.connection?.on("received", (message: Message) => {
      message.author.avatarUrl = this._imageService.fromBase64ToUrl(message.author.avatar);
      this.currentChat?.messages.push(message);

      this.chats!.filter(_ => _.id === this.currentChat?.id)[0].messages[0] = message; //меняем сообщение на новое в стобце слева 

    });
  }

  ngAfterViewChecked() {
    this.scrollToBottom();
  }

  send() {
    if (!this._messageBody.nativeElement.value.trim()) {
      this._messageBody.nativeElement.value = "";
      return;
    }
    const message: Message = {
      body: this._messageBody.nativeElement.value,
      creationDate: new Date(),
      id: Guid.createEmpty().toString(),
      chatId: this.currentChat?.id!,
      author: {
        id: this._userStorage.user?.id!,
        userName: this._userStorage.user?.userName!,
        avatarUrl: this._userStorage.user?.avatarUrl!,
        avatar: ""
      }
    }

    this.connection?.invoke('NewMessage', message);
    this._messageBody.nativeElement.value = "";
  }

  openChat(id: string) {
    this._chatService.getChat(id).subscribe(res => {

      res.members.map(a => a.avatarUrl = this._imageService.fromBase64ToUrl(a.avatar));
      res.messages.map(a => a.author.avatarUrl = this._imageService.fromBase64ToUrl(a.author.avatar));

      this.currentChat = res;
      this.membersExceptYou = res.members.filter(_ => _.id !== this._userStorage.user?.id)
    })
  }


  scrollToBottom() {
    if (this._content)
      this._content.nativeElement.scrollTop = this._content.nativeElement.scrollHeight;
  }
}
