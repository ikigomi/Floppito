import { Injectable } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { UserStorageService } from '../services/user-storage.service';

@Injectable({
  providedIn: 'root'
})
export class CurrentUserGuard implements CanActivate {
  constructor(private _userStorage: UserStorageService, private _router:Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const id = route.params['id'];
    if (id === this._userStorage.user?.id) {
      return true;
    }
    this._router.navigateByUrl('/');
    return false;
  }

}
