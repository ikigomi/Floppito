import { Experience } from "./Enums/Experience";
import { Schedule } from "./Enums/Schedule";
import { Sorting } from "./Enums/Sorting";

export interface FilterModel {
  searchString?:string;
  searchInTitle?:boolean;
  searchInDescription?:boolean;
  salaryFrom?:number;
  workExperience?:Experience[];
  workSchedule?:Schedule[];
  sortBy?:Sorting;
}