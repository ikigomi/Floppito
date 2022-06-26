import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { HttpClientModule } from '@angular/common/http';
import { FooterComponent } from './components/footer/footer.component';
import { PostsComponent } from './components/posts/posts.component';
import { PostsListComponent } from './components/posts/posts-list/posts-list.component';
import { PostEditComponent } from './components/posts/post-edit/post-edit.component';
import { PostEditFormComponent } from './components/posts/post-edit/post-edit-form/post-edit-form.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PostDetailsComponent } from './components/posts/post-details/post-details.component';
import { PostsTableComponent } from './components/posts/posts-table/posts-table.component';
import { CommentsSectionComponent } from './components/comments/comments-section/comments-section.component';
import { CommentsComponent } from './components/comments/comments.component';
import { CategoriesComponent } from './components/categories/categories.component';
import { CategoryTableComponent } from './components/categories/category-table/category-table.component';
import { MaterialModule } from '../material/material.module';
import { UserProfileComponent } from './components/user/user-profile/user-profile.component';
import { UserTableComponent } from './components/user/user-table/user-table.component';
import { CommentTableComponent } from './components/comments/comment-table/comment-table.component';
import { UserComponent } from './components/user/user.component';
import { ArrayPipe } from './pipes/array.pipe';
import { EnumPipe } from './pipes/enum.pipe';
import { ChatComponent } from './components/chat/chat.component';
import { UserProfileSettingsComponent } from './components/user/user-profile/user-profile-settings/user-profile-settings.component';
import { ChangePasswordFormComponent } from './components/user/user-profile/user-profile-settings/change-password-form/change-password-form.component';
import { SearchComponent } from './components/search/search.component';
import { SearchFormComponent } from './components/search/search-form/search-form.component';


@NgModule({
  declarations: [
    HeaderComponent,
    FooterComponent,
    PostsComponent,
    PostsListComponent,
    PostEditComponent,
    PostEditFormComponent,
    PostDetailsComponent,
    PostsTableComponent,
    CommentsSectionComponent,
    CommentsComponent,
    CategoriesComponent,
    CategoryTableComponent,
    UserProfileComponent,
    UserTableComponent,
    CommentTableComponent,
    UserComponent,
    ArrayPipe,
    EnumPipe,
    ChatComponent,
    UserProfileSettingsComponent,
    ChangePasswordFormComponent,
    SearchComponent,
    SearchFormComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    ReactiveFormsModule,
    MaterialModule,
    FormsModule
  ],
  exports: [
    HeaderComponent,
    FooterComponent,
    PostsTableComponent,
    MaterialModule,
    UserTableComponent,
    CategoryTableComponent,
    CommentTableComponent
  ]
})
export class CoreModule { }