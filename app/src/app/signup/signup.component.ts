import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SocialUser } from 'angularx-social-login';
import { AuthService } from '../core/services/auth.service';
import { NotificationService } from '../core/services/notification.service';
import { ErrorModel } from '../models/ErrorModel';
import { ExternalLoginModel } from '../models/Identity/ExternalLoginModel';
import { RegisterModel } from '../models/Identity/RegisterModel';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  errors: ErrorModel[] = [];

  constructor(private _authService: AuthService, private _router: Router, private _notificationService:NotificationService) { }

  ngOnInit(): void {
    if (this._authService.isAuth)
      this._authService.logout();
  }

  signup(signupModel: RegisterModel) {
    
    this.errors = [];    
    this._authService.signup(signupModel).subscribe(res => {
      this._notificationService.showSuccess("На указанный email отправлено сообщение с подтверждением аккаунта")
      this._router.navigateByUrl('/login');
    }, err => {     
      this.errors=err.error.errors;
    });

  }

  

}
