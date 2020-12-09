import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PharmacistGuard implements CanActivate {
  constructor(private toast: ToastrService){

  }
  
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if(localStorage.getItem('username') !== null && localStorage.getItem('role') == 'Pharmacist'){
        return true;
      }
      this.toast.error("You are not in pharmacist role");
      return false;
  }
  
}
