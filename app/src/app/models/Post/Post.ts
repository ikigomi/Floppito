import { BaseModel } from "../Base/BaseModel";
import { Comment } from 'src/app/models/Comment/Comment';
import { SafeUrl } from "@angular/platform-browser";
import { Author } from "../Author/Author";
import { Experience } from "../Enums/Experience";
import { Schedule } from "../Enums/Schedule";
import { MoneyCode } from "../Enums/MoneyCode";


export interface Post extends BaseModel{
  title:string;
  description?:string;
  author:Author;
  images:string[];
  categoryId:string;
  categoryTitle:string;
  comments?:Comment[];
  creationDate:Date;
  salaryFrom:number;
  salaryTo:number;
  workExperience:Experience[];
  workSchedule:Schedule[];
  moneyCode:MoneyCode;
}