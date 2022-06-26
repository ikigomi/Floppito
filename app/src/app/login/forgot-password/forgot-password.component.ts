import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { ErrorModel } from 'src/app/models/ErrorModel';
import { ForgotPasswordModel } from 'src/app/models/Identity/ForgotPasswordModel';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent {
  errors: ErrorModel[] = [];

  constructor(private _authService: AuthService,
    private _notificationService: NotificationService,
    private _router: Router,) { }



  forgotPassword(forgotPasswordModel: ForgotPasswordModel) {    
    this._authService.forgotPassword(forgotPasswordModel).subscribe(res => {
        this._notificationService.showSuccess("Письмо с дальнейшими инструкциями отправлено. Проверьте указанный email");
        this._router.navigateByUrl("/");
      },
        err => {         
          this.errors = err.error.errors;
        })
  }
}
