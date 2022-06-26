import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-forgot-password-form',
  templateUrl: './forgot-password-form.component.html',
  styleUrls: ['./forgot-password-form.component.scss']
})
export class ForgotPasswordFormComponent implements OnInit {

  @Output() submitEvent = new EventEmitter();


  forgotPasswordForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.forgotPasswordForm = this.fb.group({
      'clienturi':'',
      'email': ['', [Validators.required, Validators.email]]
    })
  }

  get emailControl() {
    return this.forgotPasswordForm.get('email');
  }

  ngOnInit(): void {
  }

  submit() {
    this.forgotPasswordForm.value['clienturi'] =  'http://localhost:4200/login/resetpassword';
    this.submitEvent.next(this.forgotPasswordForm.value);
  }

}
