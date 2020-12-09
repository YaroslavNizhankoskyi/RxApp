import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  registerMode = false;

  constructor(private http: HttpClient, 
    private router: Router) { }

  ngOnInit() {
  }

  workToggle(){
    
    const role = localStorage.getItem('role');
    console.log(role);
    switch(role){
      case "Admin":
        this.router.navigateByUrl('/admin');
        break;
      case "Pharmacist":
        this.router.navigateByUrl('/pharmacist');
        break;
      case "Medic":
        this.router.navigateByUrl('/medic');
        break;
    }
  }
  getUserName() : string | null{
    return localStorage.getItem('username')
  }

  registerToggle() {
    this.registerMode = true;
  }

  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }

}
