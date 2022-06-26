import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss']
})
export class LoginFormComponent {

  @Output() submitEvent = new EventEmitter();
  @Output() externalLoginEvent = new EventEmitter();


  loginForm:FormGroup;

  constructor (private fb:FormBuilder) {
      this.loginForm = this.fb.group({
        'email':['', [Validators.required, Validators.email]],
        'password':['', Validators.required],
      })
    }

  get emailControl() {
    return this.loginForm.get('email');
  }
  get passwordControl() {
    return this.loginForm.get('password');
  }

  externalLogin(provider:string) {
    this.externalLoginEvent.next(provider);
  }
    

  submit() {
    if(this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.submitEvent.next(this.loginForm.value);
  }

}
