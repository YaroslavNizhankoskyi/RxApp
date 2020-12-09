import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
baseUrl = environment.apiUrl + "administration/";

constructor(private http: HttpClient) { }

getRoles(){
  return this.http.get(this.baseUrl + "roles");
}

getUsers(){
  return this.http.get(this.baseUrl + "users");
}


getAllIngredients(){
  return this.http.get(this.baseUrl + "ingredients");
}

getIngredient(name: string){
  return this.http.get(this.baseUrl + "ingredients/" + name);
}

createIngredient(name: string){
  return this.http.post(this.baseUrl + "ingredients/" + name, {});
}

deleteIngredient(id: number){
  return this.http.delete(this.baseUrl + "ingredients/" + id);
}

editIngredient(id: number, model: string){
  return this.http.put(this.baseUrl + "ingredients/" + id + "/" + model, {});
}

getAllPG(){
  return this.http.get(this.baseUrl + "pharmgroups");
}

getPG(name: string){
  return this.http.get(this.baseUrl + "pharmgroups/" + name);
}

createPG(name: string){
  return this.http.post(this.baseUrl + "pharmgroups/" + name, {});
}

deletePG(id: number){
  return this.http.delete(this.baseUrl + "pharmgroups/" + id);
}

editPG(id: number, model: string){
  return this.http.put(this.baseUrl + "pharmgroups/" + id + "/" + model, {});
}

getAllIncompatible(){
  return this.http.get(this.baseUrl + "incompatible");
}

createIncompatible(first: number, second: number){
  return this.http.post(this.baseUrl + "incompatible/" + first + "/" + second, {});
}

deleteIncompatible(id: number){
  return this.http.delete(this.baseUrl + "incompatible/" + id);

}

getUsersInRole(role: string){
  return this.http.get(this.baseUrl + "Roles/" + role);
}

editUsersInRole(roleName: string, model: any){
  return this.http.post(this.baseUrl + "Roles/" + roleName, model);
}

}


