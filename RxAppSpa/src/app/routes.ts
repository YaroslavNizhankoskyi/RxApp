import { MedicGeneralComponent } from './medic/medic-general/medic-general.component';
import { PharmacistGuard } from './_guards/pharmacist.guard';
import { MedicGuard } from './_guards/medic.guard';
import { DrugComponent } from './drug/drug/drug.component';
import { PreventUnsavedChangesAdminGuard } from './_guards/prevent-unsaved-changes-admin.guard';
import { AdminRoleComponent } from './admin/admin-role/admin-role/admin-role.component';
import { AdminGeneralComponent } from './admin/admin-general/admin-general/admin-general.component';
import { AdminGuard } from './_guards/admin.guard';
import { PharmacyComponent } from './pharmacy/pharmacy/pharmacy.component';
import { HomeComponent } from './home/home/home.component';
import { Routes, CanDeactivate, CanActivate } from '@angular/router';
import { AuthGuard } from './_guards/auth.guard';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
          {path: 'admin', component: AdminGeneralComponent, 
            canActivate: [AdminGuard],
            canDeactivate: [PreventUnsavedChangesAdminGuard]},
          {path: 'roles', component: AdminRoleComponent,
            canActivate: [AdminGuard]},
          {path: 'medic', component: MedicGeneralComponent,
            canActivate: [MedicGuard]},
          {path: 'pharmacist', component: PharmacyComponent,
            canActivate: [PharmacistGuard]},
          {path: 'drug', component: DrugComponent}
        ]
      }
];