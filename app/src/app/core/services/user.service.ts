import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/User/User';
import { environment } from 'src/environments/environment';
import { UserStorageService } from './user-storage.service';
import { Roles } from 'src/app/models/Enums/Roles';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ImageService } from './image.service';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  userUrl = environment.apiUrl + "user/";
  constructor(private _userStorage: UserStorageService,
    private _http: HttpClient,
    private _imageService: ImageService) { }

  getCurrentUser(): Observable<User> {
    return this._http.get<User>(this.userUrl + "getUser");
  }

  getUser(id: string): Observable<User> {
    return this._http.get<User>(this.userUrl + "getUser/" + id);
  }

  getUsers(): Observable<User[]> {
    return this._http.get<User[]>(this.userUrl);
  }

  addAdmins(ids: string[]): Observable<null> {
    return this._http.post<null>(this.userUrl + "AddAdmins", ids);
  }

  deleteAdmins(ids: string[]): Observable<null> {
    return this._http.post<null>(this.userUrl + "DeleteAdmins", ids);
  }

  saveUserData(user?:User) {
    if(user) {
      user.avatarUrl=this._imageService.fromBase64ToUrl(user.avatar);
      this._userStorage.user=user;
      return;
    }

    if(!this._userStorage.user && localStorage.getItem('token'))
      this.getCurrentUser().subscribe(res => {
        res.avatarUrl=this._imageService.fromBase64ToUrl(res.avatar);
        this._userStorage.user=res;
      }, err => {
        localStorage.clear();
      })
  }

  clearUserData() {
    this._userStorage.user = undefined;
  }
}
