import { LowerCasePipe } from '@angular/common';
import { Pipe, PipeTransform, Type } from '@angular/core';
import { Experience } from 'src/app/models/Enums/Experience';
import { Gender } from 'src/app/models/Enums/Gender';
import { MoneyCode } from 'src/app/models/Enums/MoneyCode';
import { Roles } from 'src/app/models/Enums/Roles';
import { Schedule } from 'src/app/models/Enums/Schedule';

@Pipe({
  name: 'enum'
})
export class EnumPipe implements PipeTransform {

  transform(value: number | number[], type: string): string[] {

    if (typeof value === 'number') {
      return [this.enumToString(value, type)];
    }

    return value.map(x => this.enumToString(x, type));
  }

  enumToString(value: number, type: string): any {

    switch (type) {
      case 'Gender':
        return Gender[value];

      case 'Roles':
        return Roles[value];

      case 'MoneyCode':
        return Object.values(MoneyCode)[value];

      case 'Experience':
        let keysExperience=Object.values(Experience);   
        return keysExperience.filter(e => keysExperience.indexOf(e) % 2 == 0)[value];

      case 'Schedule':
        let keysSchedule=Object.values(Schedule);   
        return keysSchedule.filter(s => keysSchedule.indexOf(s) % 2 == 0)[value];


      default:
        return "Enum not found";
    }
  }


}
