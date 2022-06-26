import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FilterModel } from 'src/app/models/FilterModel';
import { Post } from 'src/app/models/Post/Post';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PostService {
  postUrl = environment.apiUrl+'post/'

  constructor(private _http: HttpClient) { }

  getPosts():Observable<Post[]> {
    return this._http.get<Post[]>(this.postUrl);
  }

  getPost(id:string):Observable<Post> {
    return this._http.get<Post>(this.postUrl+id);
  }

  getNewPosts():Observable<Post[]> {
    return this._http.get<Post[]>(this.postUrl+'getNewPosts/');
  }

  getRejectedPosts():Observable<Post[]> {
    return this._http.get<Post[]>(this.postUrl+'getRejectedPosts/');
  }

  getPostWithComments(id:string):Observable<Post> {
    return this._http.get<Post>(this.postUrl+'getPostWithComments/'+id);
  }

  getPostsByCategory(categoryId:string):Observable<Post[]> {
    return this._http.get<Post[]>(this.postUrl+'getPostsByCategory?categoryId='+categoryId);
  }

  getPostsByUser(userId:string):Observable<Post[]> {
    return this._http.get<Post[]>(this.postUrl+'getPostsByUser?userId='+userId);
  }

  getPostsBySearchQuery(query:string):Observable<Post[]> {
    return this._http.get<Post[]>(this.postUrl+'getPostsBySearch'+query);
  }

  add(post: Post):Observable<Post> {
    return this._http.post<Post>(this.postUrl, post);
  }

  delete(id: string):Observable<null> {
    return this._http.delete<null>(this.postUrl+id);
  }

  deleteRange(ids: string[]):Observable<null> {
    return this._http.post<null>(this.postUrl+'deleteRange', ids);
  }

  update(post: Post):Observable<null> {
    return this._http.put<null>(this.postUrl, post);
  }

  acceptPosts(ids:string[]):Observable<null> {
    return this._http.post<null>(this.postUrl+'acceptPosts', ids);
  }

  rejectPosts(ids:string[]):Observable<null> {
    return this._http.post<null>(this.postUrl+'rejectPosts', ids);
  }
}
