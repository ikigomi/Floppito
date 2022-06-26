import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor(private _toastr:ToastrService) { }

  showSuccess(message?:string, title?:string) {
    this._toastr.success(message, title);
  }

  showError(message?:string, title?:string) {
    this._toastr.error(message, title);
  }

  showInfo(message?:string, title?:string) {
    this._toastr.info(message, title);
  }

  showWarning(message?:string, title?:string) {
    this._toastr.warning(message, title);
  }
}
