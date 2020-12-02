import { HttpClient, HttpClientModule } from '@angular/common/http';
import { DrugService } from './_services/drug.service';
import { RecipeService } from './_services/recipe.service';
import { AdminService } from './_services/admin.service';
import { CustomValidatorService } from 'src/app/_services/custom-validator.service';
import { AuthService } from 'src/app/_services/auth.service';
import { MedicineComponent } from './medicine/medicine/medicine.component';
import { NavComponent } from './nav/nav/nav.component';

import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { JwtModule } from '@auth0/angular-jwt';
import { appRoutes } from './routes';
import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register/register.component';
import { HomeComponent } from './home/home/home.component';

export function tokenGetter() {
  return localStorage.getItem('token');
}


@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    HomeComponent,
    NavComponent,
    MedicineComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter
        //allowedDomains: ['localhost:5002'],
        //disallowedRoutes: ['localhost:5002/api/auth'],
      },
    }),
  ],
  providers: [
    AuthService,
    CustomValidatorService,
    AdminService,
    RecipeService,
    DrugService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }