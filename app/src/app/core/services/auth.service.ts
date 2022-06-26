import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GoogleLoginProvider, SocialAuthService } from 'angularx-social-login';
import { Observable } from 'rxjs';
import { ChangePasswordModel } from 'src/app/models/Identity/ChangePasswordModel';
import { ExternalLoginModel } from 'src/app/models/Identity/ExternalLoginModel';
import { ForgotPasswordModel } from 'src/app/models/Identity/ForgotPasswordModel';
import { LoginResponse } from 'src/app/models/Identity/LoginResponse';
import { RegisterModel } from 'src/app/models/Identity/RegisterModel';
import { ResetPasswordModel } from 'src/app/models/Identity/ResetPasswordModel';
import { environment } from 'src/environments/environment';
import { LoginModel } from '../../models/Identity/LoginModel';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  identityUrl = environment.apiUrl + "identity/";


  constructor(private _userService: UserService, 
              private _http: HttpClient, 
              private _externalAuthService: SocialAuthService ) { }

  login(model: LoginModel): Observable<LoginResponse> {
    return this._http.post<LoginResponse>(this.identityUrl + 'login/', model);
  }

  resetPassword(model: ResetPasswordModel): Observable<null> {
    return this._http.post<null>(this.identityUrl + 'resetPassword/', model);
  }

  forgotPassword(model: ForgotPasswordModel): Observable<null> {    
    return this._http.post<null>(this.identityUrl + 'forgotPassword/', model);
  }

  loginWithGoogle() {
    return this._externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  }

  externalLogin (body: ExternalLoginModel) {
    return this._http.post<LoginResponse>(this.identityUrl+"externalLogin/", body);
  }

  signup(model: RegisterModel): Observable<null> {
    return this._http.post<null>(this.identityUrl + 'signup/', model);
  }

  changePassword(model: ChangePasswordModel): Observable<null> {
    return this._http.post<null>(this.identityUrl + 'changePassword/', model);
  }

  logout() {
    localStorage.clear();
    this._userService.clearUserData();
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  get token() {
    return localStorage.getItem('token');
  }

  get isAuth() {
    return !!this.token;
  }
}
