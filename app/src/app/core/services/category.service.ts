import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Category } from 'src/app/models/Category/Category';
import { environment } from 'src/environments/environment';
import { Guid } from "guid-typescript";

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  categoryUrl = environment.apiUrl+'category/'

  constructor(private _http: HttpClient) { }

  getCategories():Observable<Category[]> {
    return this._http.get<Category[]>(this.categoryUrl);
  }

  add(category: Category):Observable<Category> {
    return this._http.post<Category>(this.categoryUrl, category);
  }

  delete(id: string):Observable<null> {
    return this._http.delete<null>(this.categoryUrl+id);
  }

  deleteRange(ids: string[]):Observable<null> {
    return this._http.post<null>(this.categoryUrl+"deleteRange", ids);
  }

  update(category: Category):Observable<null> {
    return this._http.put<null>(this.categoryUrl, category);
  }
}
