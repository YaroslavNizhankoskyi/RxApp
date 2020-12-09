import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class CustomValidatorService {

constructor() { }

passwordMatchValidator(password: string, confirmPassword: string) {
  return (formGroup: FormGroup) : null => {
    const passwordControl = formGroup.controls[password];
    const confirmPasswordControl = formGroup.controls[confirmPassword];

    if (!passwordControl || !confirmPasswordControl) {
      return null;
    }

    if (passwordControl.value !== confirmPasswordControl.value) {
      confirmPasswordControl.setErrors({ passwordMismatch: true });
    } else {
      confirmPasswordControl.setErrors(null);
    }

    return null;
  };
}

}
