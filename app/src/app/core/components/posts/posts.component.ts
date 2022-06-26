import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { Post } from 'src/app/models/Post/Post';
import { PostService } from '../../services/post.service';
import { map, switchMap } from 'rxjs/operators'
import { Observable } from 'rxjs';
import { Guid } from "guid-typescript";
import { FilterModel } from 'src/app/models/FilterModel';
import { Sorting } from 'src/app/models/Enums/Sorting';


@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent implements OnInit {
  posts?:Post[];
  posts$?:Observable<Post[]>;
  userId:string | null=null; 
  categoryId:string | null=null;
  searchString:string | null=null;
  
  constructor(private _postService:PostService, 
              private _route:ActivatedRoute,
              private _router:Router) { }

  //не отправляет новый запрос при повторном поиске

  // ngOnInit(): void {   
  //   this._route.queryParamMap.pipe(
  //     map<ParamMap, void>(params=> {
  //       this.searchString = params.get('searchString');
  //       this.categoryId = params.get('categoryId');
  //     }),
  //     switchMap<void, Observable<Post[]>>(() => 
  //       this.searchString ? this._postService.getPostsBySearch(this.searchString) :
  //       this.categoryId ? this._postService.getPostsByCategory(this.categoryId) :
  //       this._postService.getPosts()))
  //   .subscribe(res => {     
  //     this.posts=res;
  //   });
  // }

  ngOnInit(): void {
    this._route.queryParams.subscribe(params => {  
      let indexOfQuery = this._router.url.indexOf('?');
      let query="";
      if(indexOfQuery != -1)
        query = this._router.url.slice(this._router.url.indexOf('?'));
         
      this.categoryId = params['categoryId'];
      this.userId = params['userId'];   
    
      if(this.categoryId) { 
        this.posts$ = this._postService.getPostsByCategory(this.categoryId);
      }

      else if(this.userId) { 
        this.posts$ = this._postService.getPostsByUser(this.userId);
      }

      else if(query) {
        this.posts$ = this._postService.getPostsBySearchQuery(query)
      }
  
      else {
        this.posts$ = this._postService.getPosts();
      }
    });
  } 

}
