import { SafeUrl } from "@angular/platform-browser";
import { BaseModel } from "../Base/BaseModel";
import { Gender } from "../Enums/Gender";
import { Roles } from "../Enums/Roles";

export interface User extends BaseModel{
  userName:string;
  name:string;
  birthDate:Date;
  gender:Gender;
  avatar:string;
  phoneNumber:string;
  avatarUrl:SafeUrl;
  email:string
  roles:Roles[]
}