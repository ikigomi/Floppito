import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Comment } from 'src/app/models/Comment/Comment';
import { Guid } from "guid-typescript";

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  commentUrl = environment.apiUrl+"comment/";

  constructor(private _http:HttpClient) { }

  getCommentsByPost(postId:string):Observable<Comment[]> {
    return this._http.get<Comment[]>(this.commentUrl+postId);
  }

  getComments():Observable<Comment[]> {
    return this._http.get<Comment[]>(this.commentUrl);
  }
  
  add(comment: Comment):Observable<Comment> {
    return this._http.post<Comment>(this.commentUrl, comment);
  }

  delete(id: string):Observable<null> {
    return this._http.delete<null>(this.commentUrl+id);
  }

  update(comment: Comment):Observable<null> {
    return this._http.put<null>(this.commentUrl, comment);
  }

  deleteRange(ids: string[]):Observable<null> {
    return this._http.post<null>(this.commentUrl+"deleteRange", ids);
  }
}
