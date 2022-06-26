import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Category } from 'src/app/models/Category/Category';
import { AuthService } from '../../services/auth.service';
import { CategoryService } from '../../services/category.service';
import { PostService } from '../../services/post.service';
import { UserStorageService } from '../../services/user-storage.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  categories!:Category[];
  logoPath:string = environment.logoUrl;

  constructor(private _categoryService:CategoryService,
              private _authService:AuthService, 
              private _router:Router,
              public _userStorage:UserStorageService) { }

  ngOnInit(): void {    
    this._categoryService.getCategories().subscribe(res => {
      this.categories=res;     
    }, err => {
      
    });
  }

  logout() {
    this._authService.logout();
    this._router.navigateByUrl("/");
  }

  search(searchString:string) {       
    this._router.navigate(['/posts/'], { queryParams: { searchString: searchString } });
  }

  byCategory(categoryId:string) {
    this._router.navigate(['/posts/'], { queryParams: { categoryId: categoryId } });

  }
}
