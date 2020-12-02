import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';
import { CustomValidatorService } from 'src/app/_services/custom-validator.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  

  
 @Output() cancelRegister = new EventEmitter<boolean>();
  user = {
    email: "gmail.com",
    username: "name",
    password: "Passw0rd",
    confirmPassword: "Passw0rd"
  };

  registerForm = this.fb.group({
      password : ['',
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(20),
      ],
      email: [''],
      username:  ['',
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(20)
      ],
      confirmPassword: ['',
        Validators.required,
      ]
    },
    {
      validator: this.customValidator.passwordMatchValidator('password', 'confirmPassword')
    });

  register() {
    if(this.registerForm.valid){
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe( () => {

      });
    }
    console.log();
  }

  constructor(private authService: AuthService,
    private fb: FormBuilder,
    private router: Router,
    private customValidator: CustomValidatorService) { }

  ngOnInit() {
  }

  
  cancel() {
     this.cancelRegister.emit(false);
  }

}
