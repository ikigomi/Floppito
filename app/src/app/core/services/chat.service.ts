import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Chat } from 'src/app/models/Chat/Chat';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  chatUrl = environment.apiUrl + "chat/";

  constructor(private _http: HttpClient) { }

  add(chat: Chat): Observable<Chat> {
    return this._http.post<Chat>(this.chatUrl, chat);
  }

  getChat(id: string): Observable<Chat> {
    return this._http.get<Chat>(this.chatUrl + id);
  }

  getChatsByUser(userId: string): Observable<Chat[]> {
    return this._http.get<Chat[]>(this.chatUrl + "getByUser/" + userId);
  }

  getIdByUsers(userIds: string[]): Observable<string> {
    let params = new HttpParams();
    for (const userId of userIds) {
      params = params.append('userId', userId);
    }
    return this._http.get<string>(this.chatUrl + "GetIdByUsers/", { params: params });
  }
}
