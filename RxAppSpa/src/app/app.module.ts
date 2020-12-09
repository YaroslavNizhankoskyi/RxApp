import { RecipeInfoComponent } from './recipe/recipe-component/recipe-info/recipe-info/recipe-info.component';
import { DrugRecipeComponent } from './drug/drug/drug-recipe/drug-recipe/drug-recipe.component';
import { UserExistsComponent } from './user-exists/user-exists/user-exists.component';
import { MedicGeneralComponent } from './medic/medic-general/medic-general.component';
import { DrugCreateComponent } from './drug/drug/drug-create/drug-create/drug-create.component';
import { DrugEditComponent } from './drug/drug/drug-edit/drug-edit/drug-edit.component';
import { DrugInfoComponent } from './drug/drug/drug-info/drug-info/drug-info.component';
import { DrugComponent } from './drug/drug/drug.component';
import { AdminGuard } from './_guards/admin.guard';
import { PreventUnsavedChangesAdminGuard } from './_guards/prevent-unsaved-changes-admin.guard';
import { AdminRoleComponent } from './admin/admin-role/admin-role/admin-role.component';
import { AdminGeneralComponent } from './admin/admin-general/admin-general/admin-general.component';
import { PharmacyComponent } from './pharmacy/pharmacy/pharmacy.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { DrugService } from './_services/drug.service';
import { RecipeService } from './_services/recipe.service';
import { AdminService } from './_services/admin.service';
import { CustomValidatorService } from 'src/app/_services/custom-validator.service';
import { AuthService } from 'src/app/_services/auth.service';
import { NavComponent } from './nav/nav/nav.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { PaginationModule } from 'ngx-bootstrap/pagination';


import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { JwtModule } from '@auth0/angular-jwt';
import { appRoutes } from './routes';
import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register/register.component';
import { HomeComponent } from './home/home/home.component';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { ErrorInterceptor } from './_interceptors/error.interceptor';
import { AuthInterceptor } from './_interceptors/auth.interceptor';
import { NgbModule, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import {TranslateLoader, TranslateModule, TranslateService} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import { SortableModule } from 'ngx-bootstrap/sortable';
import { DragDropModule} from '@angular/cdk/drag-drop';
import { RecipeComponent } from './recipe/recipe/recipe.component';


export function tokenGetter() {
  return localStorage.getItem('token');
}

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}


@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    HomeComponent,
    NavComponent,
    AdminGeneralComponent,
    AdminRoleComponent,
    PharmacyComponent,
    DrugComponent,
    DrugInfoComponent,
    DrugEditComponent,
    DrugCreateComponent,
    MedicGeneralComponent,
    UserExistsComponent,
    DrugRecipeComponent,
    RecipeComponent,
    RecipeInfoComponent
  ],
  entryComponents:[
    DrugInfoComponent,
    DrugEditComponent,
    DrugCreateComponent,
    UserExistsComponent,
    DrugRecipeComponent,
    RecipeInfoComponent
  ],
  imports: [
    DragDropModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    SortableModule.forRoot(),
    CommonModule,
    FormsModule,
    PaginationModule,
    ReactiveFormsModule,
    BrowserModule,
    NgbModule,
    HttpClientModule,
    BsDropdownModule.forRoot(),
    RouterModule.forRoot(appRoutes),
    JwtModule.forRoot({config: {
      tokenGetter: tokenGetter,
      allowedDomains: ['localhost:44360'],
      disallowedRoutes: ['localhost:44360/api/account'],
      authScheme: "Bearer "
    }}),
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    AuthService,
    CustomValidatorService,
    AdminService,
    TranslateService,
    RecipeService,
    DrugService,
    AdminGuard,
    PreventUnsavedChangesAdminGuard
  
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(translate: TranslateService) {
    translate.setDefaultLang('en');
  }
 }