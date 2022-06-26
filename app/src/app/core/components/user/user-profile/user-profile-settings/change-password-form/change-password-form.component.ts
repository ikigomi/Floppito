import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserStorageService } from 'src/app/core/services/user-storage.service';
import { CustomValidators } from 'src/app/core/validators/CustomValidators';

@Component({
  selector: 'app-change-password-form',
  templateUrl: './change-password-form.component.html',
  styleUrls: ['./change-password-form.component.scss']
})
export class ChangePasswordFormComponent {

  @Output() changePasswordEvent = new EventEmitter();
  
  password: FormControl;
  confirmPassword: FormControl;
  changePasswordForm: FormGroup;
  hideOld = true;
  hideNew = true;
  
  constructor(private fb: FormBuilder, private _userStorage:UserStorageService) {

    this.password = new FormControl('', [Validators.required, Validators.minLength(6)]);
    this.confirmPassword = new FormControl('', [Validators.required, CustomValidators.mustMatch(this.password)]);

    this.changePasswordForm = this.fb.group({
      'email':[_userStorage.user?.email, [Validators.required, Validators.email]],
      'oldPassword': ['', [Validators.required, Validators.minLength(6)]],
      'newPassword': this.password,
      'confirmNewPassword': this.confirmPassword,
    })
  }

  get oldPasswordControl() {
    return this.changePasswordForm.get('oldPassword');
  }

  get newPasswordControl() {
    return this.changePasswordForm.get('newPassword');
  }

  get confirmNewPasswordControl() {
    return this.changePasswordForm.get('confirmNewPassword');
  }


  submit() {
    if(this.changePasswordForm.invalid) {
      this.changePasswordForm.markAllAsTouched();
      return;
    }

    this.changePasswordEvent.next(this.changePasswordForm.value);
  }


}
