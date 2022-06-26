import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './admin/admin/admin.component';
import { ChatComponent } from './core/components/chat/chat.component';
import { PostDetailsComponent } from './core/components/posts/post-details/post-details.component';
import { PostEditComponent } from './core/components/posts/post-edit/post-edit.component';
import { PostsComponent } from './core/components/posts/posts.component';
import { SearchComponent } from './core/components/search/search.component';
import { UserProfileSettingsComponent } from './core/components/user/user-profile/user-profile-settings/user-profile-settings.component';
import { UserProfileComponent } from './core/components/user/user-profile/user-profile.component';
import { AdminGuard } from './core/guards/admin.guard';
import { AuthGuard } from './core/guards/auth.guard';
import { CurrentUserGuard } from './core/guards/current-user.guard';
import { UserResolver } from './core/resolvers/user.resolver';
import { ForgotPasswordComponent } from './login/forgot-password/forgot-password.component';
import { LoginComponent } from './login/login/login.component';
import { ResetPasswordComponent } from './login/reset-password/reset-password.component';
import { SignupComponent } from './signup/signup.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'login/resetpassword', component: ResetPasswordComponent },
  { path: 'login/forgotpassword', component: ForgotPasswordComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'posts', component: PostsComponent },
  { path: 'post/edit', component: PostEditComponent, canActivate: [AuthGuard] },
  { path: 'post/edit/:id', component: PostEditComponent, canActivate: [AuthGuard] },
  { path: 'post/:id', component: PostDetailsComponent },
  { path: 'user/settings/:id', component: UserProfileSettingsComponent, canActivate: [AuthGuard, CurrentUserGuard] },
  { path: 'user/:id', component: UserProfileComponent },
  { path: 'admin', component: AdminComponent, canActivate: [AuthGuard, AdminGuard] },
  { path: 'chat', component: ChatComponent, canActivate: [AuthGuard], resolve: { user: UserResolver } },
  { path: 'search', component: SearchComponent},
  { path: '**', redirectTo: 'posts', pathMatch:'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [UserResolver]
})
export class AppRoutingModule { }
