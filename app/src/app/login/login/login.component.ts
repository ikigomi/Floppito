import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SocialUser } from 'angularx-social-login';
import { AuthService } from 'src/app/core/services/auth.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { UserService } from 'src/app/core/services/user.service';
import { ErrorModel } from 'src/app/models/ErrorModel';
import { ExternalLoginModel } from 'src/app/models/Identity/ExternalLoginModel';
import { ForgotPasswordModel } from 'src/app/models/Identity/ForgotPasswordModel';
import { LoginModel } from 'src/app/models/Identity/LoginModel';
import { ResetPasswordModel } from 'src/app/models/Identity/ResetPasswordModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  errors: ErrorModel[] = [];


  constructor(private _authService: AuthService,
    private _userService: UserService,
    private _router: Router,
    private _notificationService: NotificationService) { }

  ngOnInit(): void {
    if (this._authService.isAuth)
      this._authService.logout();
  }

  login(loginModel: LoginModel) {
    this.errors = [];

    this._authService.login(loginModel).subscribe(res => {
      this._authService.saveToken(res.token);
      this._userService.saveUserData(res.user);
      this._router.navigateByUrl('/');
    }, err => {
      this.errors = err.error.errors;
    });
  }

  externalLogin(provider: string) {
    switch (provider) {
      case 'google':
        this.loginThroughGoogle();
        break;
    }
  }

  private loginThroughGoogle() {
    this._authService.loginWithGoogle()
      .then(res => {
        const user: SocialUser = { ...res };
        const externalAuth: ExternalLoginModel = {
          provider: user.provider,
          idToken: user.idToken
        }
        this.validateExternalAuth(externalAuth);
      }, error => console.log(error))
  }

  private validateExternalAuth(externalAuth: ExternalLoginModel) {
    this._authService.externalLogin(externalAuth)
      .subscribe(res => {
        this._authService.saveToken(res.token);
        this._userService.saveUserData(res.user);
        this._router.navigateByUrl('/');
      }, err => {
        this.errors = err.error.errors;
      });
  }
}

