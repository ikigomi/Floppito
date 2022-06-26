import { SafeUrl } from "@angular/platform-browser";
import { BaseModel } from "../Base/BaseModel";

export interface Author extends BaseModel {
  userName:string;
  avatar:string;
  avatarUrl:SafeUrl;
}