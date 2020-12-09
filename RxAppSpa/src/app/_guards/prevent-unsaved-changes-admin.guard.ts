import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanDeactivate } from '@angular/router';
import { Observable } from 'rxjs';
import { AdminGeneralComponent } from '../admin/admin-general/admin-general/admin-general.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesAdminGuard implements CanDeactivate<AdminGeneralComponent> {
  canDeactivate(component: AdminGeneralComponent) {
    if (component.pharmGroups.dirty) {
        return confirm('Are you sure you want to continue?  Any unsaved changes will be lost');
    }
    return true;
}
  
}
