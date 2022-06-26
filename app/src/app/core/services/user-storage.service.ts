import { Injectable, OnInit } from '@angular/core';
import { Roles } from 'src/app/models/Enums/Roles';
import { User } from 'src/app/models/User/User';


@Injectable({
  providedIn: 'root'
})
export class UserStorageService{
  user?:User;
  constructor() { }

  
  isAdmin() {
    return !!this.user && this.user!.roles.includes(Roles.Admin);
  }
}
