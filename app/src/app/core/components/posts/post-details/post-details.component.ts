import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PostService } from 'src/app/core/services/post.service';
import { Post } from 'src/app/models/Post/Post';
import { Guid } from "guid-typescript";
import { ImageService } from 'src/app/core/services/image.service';
import { UserStorageService } from 'src/app/core/services/user-storage.service';
import { NotificationService } from 'src/app/core/services/notification.service';

@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrls: ['./post-details.component.scss']
})
export class PostDetailsComponent implements OnInit {

  post?: Post;
  id?: string;
  constructor(private _postService: PostService,
    private _route: ActivatedRoute,
    private _router: Router,
    public _userStorage: UserStorageService,
    private _imageService: ImageService,
    private _notificationService: NotificationService) { }

  ngOnInit(): void {
    this.id = this._route.snapshot.params['id'];

    if (this.id) {
      this._postService.getPostWithComments(this.id).subscribe(res => {
        this.post = res;
        this.post.author.avatarUrl = this._imageService.fromBase64ToUrl(this.post.author.avatar);
        this.post.comments?.map(x => x.author.avatarUrl = this._imageService.fromBase64ToUrl(x.author.avatar));
      });
    }


  }

  delete(id: string) {
    this._postService.delete(id).subscribe(res => {
      this._notificationService.showSuccess("Пост успешно удален")
      this._router.navigateByUrl("/");
    });
  }

}
