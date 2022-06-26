import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';
import { map, switchMap, tap } from 'rxjs/operators';
import { ChatService } from 'src/app/core/services/chat.service';
import { ImageService } from 'src/app/core/services/image.service';
import { UserStorageService } from 'src/app/core/services/user-storage.service';
import { UserService } from 'src/app/core/services/user.service';
import { Author } from 'src/app/models/Author/Author';
import { Chat } from 'src/app/models/Chat/Chat';
import { Roles } from 'src/app/models/Enums/Roles';
import { User } from 'src/app/models/User/User';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  id?: string;
  user?: User;
  constructor(private _route: ActivatedRoute,
              private _router:Router,
              private _userService: UserService,
              public _userStorage:UserStorageService,
              private _imageService:ImageService,
              private _chatService:ChatService) { }

  ngOnInit(): void {
    this._route.params.pipe(
      map<Params, void>(params => {
        this.id = params['id'];
      }),
      switchMap<void, Observable<User>>(() => {
          return this._userService.getUser(this.id!);
      })).subscribe(res => {
        res.avatarUrl=this._imageService.fromBase64ToUrl(res.avatar);
        this.user=res;
      })


  }

  isAdmin() {
    return this.user?.roles.includes(Roles.Admin);
  }

  createChat(userId:string) {
    const ids = [this._userStorage.user?.id!, userId];
    this._chatService.getIdByUsers(ids).subscribe(res => {
      if(res===Guid.EMPTY) {

        let members:Author[]=[];

        for (const id of ids) {
          members.push({
            id:id,
            avatar:"",
            avatarUrl:"",
            userName:""
          });
        }

        const chat:Chat = {
          id:Guid.createEmpty().toString(),
          messages:[],
          members:members,
        }
    
        this._chatService.add(chat).subscribe(res => {
          this._router.navigate(['/chat/'], { queryParams: { chatId: res.id } });
        });
      }
        
      else this._router.navigate(['/chat/'], { queryParams: { chatId: res } });
    })

  }

}
