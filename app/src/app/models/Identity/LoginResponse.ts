import { User } from "../User/User";

export interface LoginResponse {
  token:string;
  user:User;
}