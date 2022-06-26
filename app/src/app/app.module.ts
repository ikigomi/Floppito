import { forwardRef, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignupComponent } from './signup/signup.component';
import { SignupFormComponent } from './signup/signup-form/signup-form.component';
import { CoreModule } from './core/core.module';
import { NG_VALUE_ACCESSOR, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './core/interceptors/auth.interceptor';
import { AuthGuard } from './core/guards/auth.guard';
import { AdminGuard } from './core/guards/admin.guard';
import { AdminComponent } from './admin/admin/admin.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxCaptchaModule } from 'ngx-captcha';
import { ToastrModule } from 'ngx-toastr';
import { StatusCodeInterceptor } from './core/interceptors/status-code.interceptor';
import { GoogleLoginProvider, SocialAuthServiceConfig, SocialLoginModule } from 'angularx-social-login';
import { ForgotPasswordFormComponent } from './login/forgot-password/forgot-password-form/forgot-password-form.component';
import { ResetPasswordFormComponent } from './login/reset-password/reset-password-form/reset-password-form.component';
import { ResetPasswordComponent } from './login/reset-password/reset-password.component';
import { ForgotPasswordComponent } from './login/forgot-password/forgot-password.component';
import { LoginComponent } from './login/login/login.component';
import { LoginFormComponent } from './login/login/login-form/login-form.component';
import { SearchFormComponent } from './core/components/search/search-form/search-form.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    LoginFormComponent,
    SignupComponent,
    SignupFormComponent,
    AdminComponent,
    ForgotPasswordFormComponent,
    ResetPasswordFormComponent,
    ResetPasswordComponent,
    ForgotPasswordComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CoreModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    NgxCaptchaModule,
    SocialLoginModule,
    ToastrModule.forRoot()
  ],
  providers: [
    AuthGuard,
    AdminGuard,

    {
      provide: HTTP_INTERCEPTORS,
      multi: true,
      useClass: AuthInterceptor
    },

    {
      provide: HTTP_INTERCEPTORS,
      useClass: StatusCodeInterceptor,
      multi: true
    },
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '1015144935185-3jvf0b5hdsr32mbugbsbtm5fqubj377i.apps.googleusercontent.com'
            )
          },
        ],
      } as SocialAuthServiceConfig
    }],
  bootstrap: [AppComponent]
})
export class AppModule { }
