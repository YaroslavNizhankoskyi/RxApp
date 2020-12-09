import { ToastrService } from 'ngx-toastr';
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
    firstname: "firstname",
    secondname: "secondname",
    age: 18,
    password: "Passw0rd",
  };

  registerForm = this.fb.group({
      password : ['',[
        Validators.required,
        Validators.minLength(8),
        Validators.maxLength(20)
        ]
      ],
      email: ['', Validators.email],
      firstname:  ['',[
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(20)
        ]
      ],
      secondname:  ['',[
        Validators.required,
        Validators.minLength(3),
        Validators.maxLength(25),
        ]
      ],
      age: ['', [
        Validators.min(6),
        Validators.max(100)
      ]],
      confirmPassword: ['',[
        Validators.required,
      ]
      ]
    },
    {
      validator: this.customValidator.passwordMatchValidator('password', 'confirmPassword')
    });

  register() {
    if(this.registerForm.valid){
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe( () => {
        this.toast.success("Registered");
      });
    }
  }

  constructor(private authService: AuthService,
    private fb: FormBuilder,
    private router: Router,
    private customValidator: CustomValidatorService,
    private toast: ToastrService
    ) { }

  ngOnInit() {
  }

  
  cancel() {
     this.cancelRegister.emit(false);
  }

}
