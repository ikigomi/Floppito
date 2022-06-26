import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Message } from 'src/app/models/Message/Message';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  messageUrl=environment.apiUrl+"message/";
  constructor(private _http:HttpClient) { }

  update(message:Message):Observable<null> {
    return this._http.put<null>(this.messageUrl, message);
  }

  add(message:Message):Observable<Message> {
    return this._http.post<Message>(this.messageUrl, message);
  }
}
