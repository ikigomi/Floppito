import { SafeUrl } from "@angular/platform-browser";
import { Author } from "../Author/Author";
import { BaseModel } from "../Base/BaseModel";


export interface Comment extends BaseModel{
  postId:string;
  commentBody:string;
  author:Author;
  creationDate:Date;
}