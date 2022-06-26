import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PostService } from 'src/app/core/services/post.service';
import { UserStorageService } from 'src/app/core/services/user-storage.service';
import { Post } from 'src/app/models/Post/Post';
import { Guid } from "guid-typescript";
import { Category } from 'src/app/models/Category/Category';
import { CategoryService } from 'src/app/core/services/category.service';
import { Observable } from 'rxjs';
import { ImageService } from 'src/app/core/services/image.service';
import { ImagePathsResponse } from 'src/app/models/ImagePathsResponse';
import { map, switchMap } from 'rxjs/operators';
import { ErrorModel } from 'src/app/models/ErrorModel';
import { NotificationService } from 'src/app/core/services/notification.service';

@Component({
  selector: 'app-post-edit',
  templateUrl: './post-edit.component.html',
  styleUrls: ['./post-edit.component.scss']
})
export class PostEditComponent implements OnInit {
  errors: ErrorModel[] = [];
  categories: Category[] = [];
  post$?: Observable<Post>;
  id?: string;
  paths$?: Observable<ImagePathsResponse>;

  constructor(private _userStorage: UserStorageService,
    private _postService: PostService,
    private _categoryService: CategoryService,
    private _route: ActivatedRoute,
    private _router: Router,
    private _imageService: ImageService,
    private _notificationService: NotificationService) { }

  ngOnInit(): void {
    this._categoryService.getCategories().subscribe(res => {
      this.categories = res;
    })

    this.id = this._route.snapshot.params['id'];

    if (this.id) {
      this.post$ = this._postService.getPost(this.id);
    }
  }

  create(post: Post) {

    this.errors = [];
    post.author = {
      avatar: "",
      avatarUrl: "",
      id: this._userStorage.user!.id,
      userName: ""
    }
    
    this.paths$?.pipe(
      map<ImagePathsResponse, void>(p => post.images = p.paths),
      switchMap<void, Observable<Post>>(() => {
        return this._postService.add(post);
      })
    ).subscribe(res => {

    }, err => {
      this.errors = err.error.errors;
    })
  }

  edit(post: Post) {

    this._postService.update(post).subscribe(res => {
      this._notificationService.showSuccess("Пост успешно обновлен")
      this._router.navigateByUrl("post/" + post.id);
    }, err => {
      this.errors = err.error.errors;
    });
  }

  upload(files: File[]) {
    console.log("FILES", files);
    
    this.paths$ = this._imageService.upload(files);
  }

}
