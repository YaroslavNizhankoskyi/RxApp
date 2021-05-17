import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import {map} from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ReplaySubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(
      map((response: any) => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('username', response.firstName);
        localStorage.setItem('role', response.role);
        localStorage.setItem('userid', response.id);
        localStorage.setItem('allowedToAddRecipes', response.allowedAddingRecipes);
        localStorage.setItem('email', response.email);
      })
    )
  }

  register(model: any) {
    return this.http.post(this.baseUrl + 'account/register', model);
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('username')
    localStorage.removeItem('role')
    localStorage.removeItem('userid');
    localStorage.removeItem('allowedToAddRecipes');
    localStorage.removeItem('email');

  }

  findUserByEmail(email: string){
    return this.http.get(environment.apiUrl + "account/profile/" + email);
  }

  getAbilityAddRecipes(email:string){
    return this.http.get(environment.apiUrl + "account/abilitytoaddrecipes/" + email);
  }
  

}