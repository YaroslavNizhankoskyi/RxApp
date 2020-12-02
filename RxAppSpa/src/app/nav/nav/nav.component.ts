import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  model: any = {};

  constructor(public authService: AuthService, private router: Router) { }
  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(response => {
      //this.router.navigateByUrl('/members');
    })
  }

 loggedIn()
 {
   return this.authService.loggedIn();
 }

 logout()
 {
   localStorage.removeItem('token');
   //this.router.navigate(['/home']);
 }

}
