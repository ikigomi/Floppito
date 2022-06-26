import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { Observable } from 'rxjs';
import { ImagePathsResponse } from 'src/app/models/ImagePathsResponse';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImageService {

  imageUrl = environment.apiUrl+"file/";

  constructor(private _http:HttpClient,
      private _sanitizer: DomSanitizer) { }

  upload(files:File[]):Observable<ImagePathsResponse> {
    const data = new FormData();
    for (let index = 0; index < files.length; index++) {
      data.append("data", files[index], files[index].name);
      
    }
    return this._http.post<ImagePathsResponse>(this.imageUrl+'upload', data);
  }

  fromBase64ToUrl(base64String:string):SafeUrl {
    return this._sanitizer.bypassSecurityTrustResourceUrl('data:image/jpg;base64,' + base64String); 
  }

  removeUnsafe(url:string):SafeUrl {    
    return this._sanitizer.bypassSecurityTrustResourceUrl(url); 
  }
}
