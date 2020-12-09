import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { from } from 'rxjs';
import { AuthService } from 'src/app/_services/auth.service';
import { ThrowStmt } from '@angular/compiler';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  lang: string;

  model: any = {
    email: "email",
    password: "password"
  };

  loginForm = this.fb.group({
    password: ['', Validators.required],
    email: ['', Validators.required]
  })

  constructor(public translate: TranslateService,
              public authService: AuthService,
              private router: Router,
              private fb: FormBuilder,
              private toast: ToastrService,
    ) 
    {
      if(localStorage.getItem('lang'))
      {
        this.lang = localStorage.getItem('lang');
        this.translate.use(localStorage.getItem('lang'));
      }
    }

  ngOnInit() {
  }

  changeLang(value: any){
    this.lang = value;
    localStorage.setItem('lang', value);
    this.translate.use(value);
  }

  getUserName() : string | null{
    return localStorage.getItem('username')
  }

  
  getUserRole() : string | null{
    return localStorage.getItem('role')
  }

  login() {
    if(this.loginForm.valid){
      this.model = Object.assign({}, this.loginForm.value); 
      this.authService.login(this.model).subscribe(response => {
      //this.router.navigateByUrl('/medicine');
      console.log(this.getUserRole());
    })
    }
  }
 logout()
 {
  this.toast.info("Logged out")
  localStorage.removeItem('username');
  localStorage.removeItem('userid');
  localStorage.removeItem('role');
  this.router.navigate(['']);
 }


}
