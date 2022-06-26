import { Component, OnInit } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';
import { CategoryService } from 'src/app/core/services/category.service';
import { CommentService } from 'src/app/core/services/comment.service';
import { PostService } from 'src/app/core/services/post.service';
import { UserService } from 'src/app/core/services/user.service';
import { Category } from 'src/app/models/Category/Category';
import { Post } from 'src/app/models/Post/Post';
import { User } from 'src/app/models/User/User';
import { Comment } from 'src/app/models/Comment/Comment';
import { NotificationService } from 'src/app/core/services/notification.service';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {
  showPosts:boolean=false;
  showUsers:boolean=false;

  newPosts$?:Observable<Post[]>;
  rejectedPosts$?:Observable<Post[]>;
  acceptedPosts$?:Observable<Post[]>;

  users$?:Observable<User[]>;

  categories$?:Observable<Category[]>;

  comments$?:Observable<Comment[]>;

  constructor(private _postService:PostService,
              private _userService:UserService,
              private _categoryService:CategoryService,
              private _commentService:CommentService,
              private _notificationService:NotificationService) { }

  ngOnInit(): void {

    this.newPosts$ = this._postService.getNewPosts();
    this.acceptedPosts$ = this._postService.getPosts();
    this.rejectedPosts$ = this._postService.getRejectedPosts();

    this.users$ = this._userService.getUsers();

    this.categories$ = this._categoryService.getCategories();

    this.comments$ = this._commentService.getComments();
  }

  accept(ids:string[]) {
    this._postService.acceptPosts(ids).subscribe(res => {
      this._notificationService.showSuccess("Выполнено успешно");
    })
  }

  reject(ids:string[]) {
    this._postService.rejectPosts(ids).subscribe(res => {
      this._notificationService.showSuccess("Выполнено успешно");
    })
  }

  deletePosts(ids:string[]) {
    this._postService.deleteRange(ids).subscribe(res => {
      this._notificationService.showSuccess("Выполнено успешно");
    });
  }

  deleteAdmins(users:User[]) {
    const ids = users.map(p=>p.id);
    this._userService.deleteAdmins(ids).subscribe(res => {
      this._notificationService.showSuccess("Выполнено успешно");
    })
  }

  addAdmins(users:User[]) {
    const ids = users.map(p=>p.id);
        this._userService.addAdmins(ids).subscribe(res => {
          this._notificationService.showSuccess("Выполнено успешно");
    })
  }

  addCategory(title:string) {
    const category:Category = {
      id:Guid.createEmpty().toString(),
      title:title,
      creationDate:new Date()
    }
    this._categoryService.add(category).subscribe(res => {

    });
  }

  deleteCategories(ids:string[]) {
    this._categoryService.deleteRange(ids).subscribe(res => {
      this._notificationService.showSuccess("Выполнено успешно");
      
    })
  }

  deleteComments(ids:string[]) {
    this._commentService.deleteRange(ids).subscribe(res => {
      this._notificationService.showSuccess("Выполнено успешно");
      
    })
  }

}
