import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ImageService } from 'src/app/core/services/image.service';
import { CustomValidators } from 'src/app/core/validators/CustomValidators';
import { Gender } from 'src/app/models/Enums/Gender';

@Component({
  selector: 'app-signup-form',
  templateUrl: './signup-form.component.html',
  styleUrls: ['./signup-form.component.scss']
})
export class SignupFormComponent {

  @Output() submitEvent = new EventEmitter();

  password: FormControl;
  confirmPassword: FormControl;
  signupForm: FormGroup;
  gendersKeys: string[] = [];
  imageUrl?: string;
  fileToUpload: File | null = null;
  hide = true;
  siteKey = "6LdYKy0dAAAAAILJ_z9wlvWSpbGTPV9QKSnRiPRu";

  constructor(private fb: FormBuilder) {
    this.gendersKeys = Object.keys(Gender).slice(Object.keys(Gender).length / 2); //создаем массив строк из enum Gender (отсекаем первую половину ибо там int представления)
    

    this.password = new FormControl('', [Validators.required, Validators.minLength(6)]);
    this.confirmPassword = new FormControl('', [Validators.required, CustomValidators.mustMatch(this.password)]);

    this.signupForm = this.fb.group({
      'userName': ['', Validators.required],
      'name': ['', [Validators.required, Validators.pattern('^[a-zA-Zа-яА-Я ]*$')]],
      'email': ['', [Validators.required, Validators.email]],
      'password': this.password,
      'birthDate': ['', Validators.required],
      'gender': ['', Validators.required],
      'avatar': ['', ],
      'phoneNumber': ['', [Validators.pattern('[0-9]{11}')]],
      'confirmPassword': this.confirmPassword,
      'recaptcha': ['', Validators.required]
    })
  }


  get userNameControl() {
    return this.signupForm.get('userName');
  }

  get nameControl() {
    return this.signupForm.get('name');
  }

  get birthDateControl() {
    return this.signupForm.get('birthDate');
  }

  get genderControl() {
    return this.signupForm.get('gender');
  }

  get emailControl() {
    return this.signupForm.get('email');
  }

  get passwordControl() {
    return this.signupForm.get('password');
  }

  get avatarControl() {
    return this.signupForm.get('avatar');
  }

  get phoneNumberControl() {
    return this.signupForm.get('phoneNumber');
  }

  get confirmPasswordControl() {   
    
    return this.signupForm.get('confirmPassword');
  }

  get recaptchaControl() {      
    return this.signupForm.get('recaptcha');
  }




  submit() {    
    if (this.signupForm.invalid) {
      this.signupForm.markAllAsTouched();
      return;
    }
    this.signupForm.value['gender'] = (<any>Gender)[this.signupForm.value['gender']]; //меняем гендер из строки в целое для валидации на бэке

    this.submitEvent.next(this.signupForm.value);
    this.signupForm.value['gender'] = (<any>Gender)[this.signupForm.value['gender']];

  }

  handleFileInput(event: any) {

    
    this.imageUrl = undefined;
    const files = (<HTMLInputElement>event.target).files!;
    this.fileToUpload = files.item(0);
    
    if(!this.fileToUpload?.type.includes("image"))
      return;
    
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
    }
    if (this.fileToUpload) {
      reader.readAsDataURL(this.fileToUpload);

      this.fileToUpload?.arrayBuffer().then(res => {

        let buf = new Uint8Array(res);

        let binaryString = '';
        for (var i = 0; i < buf.byteLength; i++) {
          binaryString += String.fromCharCode(buf[i]);
        }

        let base64String = window.btoa(binaryString);

        this.signupForm.value['avatar'] = base64String;
      })
    }

  }

}
