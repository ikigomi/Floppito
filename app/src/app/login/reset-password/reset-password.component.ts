import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { ErrorModel } from 'src/app/models/ErrorModel';
import { ResetPasswordModel } from 'src/app/models/Identity/ResetPasswordModel';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent {

  errors: ErrorModel[] = [];


  constructor(private _authService: AuthService,
    private _router: Router,
    private _notificationService: NotificationService) { }

  resetPassword(resetPasswordModel: ResetPasswordModel) {

    this._authService.resetPassword(resetPasswordModel)
      .subscribe(res => {
        this._notificationService.showSuccess("Пароль успешно изменен")
        this._router.navigateByUrl('/login')
      },
        err => {
          this.errors = err.error.errors;
        })
  }
}
