import { Author } from "../Author/Author";
import { BaseModel } from "../Base/BaseModel";

export interface Message extends BaseModel {
  chatId:string;
  body:string;
  author:Author;
  creationDate:Date;
}