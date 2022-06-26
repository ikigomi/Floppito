import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CustomValidators } from 'src/app/core/validators/CustomValidators';

@Component({
  selector: 'app-reset-password-form',
  templateUrl: './reset-password-form.component.html',
  styleUrls: ['./reset-password-form.component.scss']
})
export class ResetPasswordFormComponent implements OnInit {

  @Output() submitEvent = new EventEmitter();


  private _token?: string;
  private _email?: string;
  hide = true;

  resetPasswordForm: FormGroup;
  password: FormControl;
  confirmPassword: FormControl;

  constructor(private fb: FormBuilder, private _route: ActivatedRoute) {

    this.password = new FormControl('', [Validators.required, Validators.minLength(6)]);
    this.confirmPassword = new FormControl('', [Validators.required, CustomValidators.mustMatch(this.password)]);

    this.resetPasswordForm = this.fb.group({
      'token':'',
      'email':'',
      'password': this.password,
      'confirmPassword': this.confirmPassword
    })
  }

  ngOnInit(): void {
    this._token = this._route.snapshot.queryParams['token'];
    this._email = this._route.snapshot.queryParams['email'];
  }

  get passwordControl() {
    return this.resetPasswordForm.get('password');
  }

  get confirmPasswordControl() {

    return this.resetPasswordForm.get('confirmPassword');
  }

  submit() {
    this.resetPasswordForm.value['token'] = this._token;
    this.resetPasswordForm.value['email'] = this._email;

    this.submitEvent.next(this.resetPasswordForm.value);

  }
}
