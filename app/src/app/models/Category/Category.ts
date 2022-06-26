import { BaseModel } from "../Base/BaseModel";

export interface Category extends BaseModel{
  title:string;
  creationDate:Date;
}