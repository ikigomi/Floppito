import { Author } from "../Author/Author";
import { BaseModel } from "../Base/BaseModel";
import { Message } from "../Message/Message";

export interface Chat extends BaseModel {
  messages:Message[];
  members:Author[];
}