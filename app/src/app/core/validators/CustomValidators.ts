import { AbstractControl, FormGroup, ValidationErrors, ValidatorFn } from "@angular/forms";

export abstract class CustomValidators {

  static mustMatch(matchingControl: AbstractControl): ValidatorFn {
    return (control: AbstractControl) => {
      // set error on matchingControl if validation fails
      if (control.value !== matchingControl.value) {
        return { mustMatch: true };
      } else {
        return null;
      }
    }
  }

}