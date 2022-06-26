import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { UserService } from 'src/app/core/services/user.service';
import { CustomValidators } from 'src/app/core/validators/CustomValidators';
import { ErrorModel } from 'src/app/models/ErrorModel';
import { ChangePasswordModel } from 'src/app/models/Identity/ChangePasswordModel';

@Component({
  selector: 'app-user-profile-settings',
  templateUrl: './user-profile-settings.component.html',
  styleUrls: ['./user-profile-settings.component.scss']
})
export class UserProfileSettingsComponent implements OnInit {

  changePasswordErrors: ErrorModel[] = [];
  
  constructor(private _authService:AuthService,
              private _notificationService:NotificationService,
              private _router:Router) {

  }


  ngOnInit(): void {
  }

  changePassword(model:ChangePasswordModel) {
    this.changePasswordErrors = [];    
    this._authService.changePassword(model).subscribe(res => {
      this._notificationService.showSuccess("Пароль успешно изменен");
      this._router.navigateByUrl("/")
    }, err => {     
      this.changePasswordErrors=err.error.errors;
    });
  }

}
