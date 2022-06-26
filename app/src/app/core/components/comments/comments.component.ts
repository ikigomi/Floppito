import { Component, Input, OnInit } from '@angular/core';
import { Comment } from 'src/app/models/Comment/Comment';
import { CommentService } from '../../services/comment.service';
import { UserStorageService } from '../../services/user-storage.service';
import { Guid } from "guid-typescript";
import { ImageService } from '../../services/image.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-comments',
  templateUrl: './comments.component.html',
  styleUrls: ['./comments.component.scss']
})
export class CommentsComponent implements OnInit {
  @Input() postId?:string;
  @Input() comments?:Comment[];

  constructor(private _commentService:CommentService, 
              private _userStorage:UserStorageService,
              private _imageService:ImageService,
              private _notificationService:NotificationService) { }

  ngOnInit(): void {    
    if(!this.comments) {
      this._commentService.getCommentsByPost(this.postId!).subscribe(res => {
        this.comments = res;
        this.comments.map(x=> x.author.avatarUrl = this._imageService.fromBase64ToUrl(x.author.avatar))
        
      }, err => {

      })
    }
    else if(this.comments.length > 0) {
      this.postId=this.comments[0].postId;
    }
    
  }

  delete(id:string) {
    this._commentService.delete(id).subscribe(res => {
      this._notificationService.showSuccess("Комментарий успешно удален");
      this.comments?.splice(this.comments.findIndex(x=>x.id===id), 1);
    }) 
  }

  add(body:string) {
    const comment:Comment = {
      id:Guid.createEmpty().toString(),
      commentBody:body,
      postId:this.postId!,
      creationDate:new Date(),
      author: {
        userName:"",
        id:this._userStorage.user?.id!,
        avatar:"",
        avatarUrl:""
      }
    }
    this._commentService.add(comment).subscribe(res => {      
      res.author.avatarUrl = this._imageService.fromBase64ToUrl(res.author.avatar);
      this.comments?.push(res);
    });
  }

  edit(body:string) {
    console.log(body);
    
  }

}
