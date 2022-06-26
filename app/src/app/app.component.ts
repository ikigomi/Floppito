import { Component, OnInit } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { UserStorageService } from './core/services/user-storage.service';
import { UserService } from './core/services/user.service';
import { icons } from './material/icons';
import { User } from './models/User/User';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private _userService: UserService,
    private _iconRegistry: MatIconRegistry,
    private _sanitizer: DomSanitizer) {
    _iconRegistry.addSvgIconLiteral('instagram', _sanitizer.bypassSecurityTrustHtml(icons.instagram));
    _iconRegistry.addSvgIconLiteral('vk', _sanitizer.bypassSecurityTrustHtml(icons.vk));
    _iconRegistry.addSvgIconLiteral('google', _sanitizer.bypassSecurityTrustHtml(icons.google));
    _iconRegistry.addSvgIconLiteral('facebook', _sanitizer.bypassSecurityTrustHtml(icons.facebook));
  }
  ngOnInit(): void {
    this._userService.saveUserData();
  }
  title = 'solarLab';
}
