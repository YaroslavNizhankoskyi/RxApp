import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Injectable } from '@angular/core';
import { HighlightSpanKind } from 'typescript';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

baseUrl = environment.apiUrl + "recipe";
userId = localStorage.getItem('userid');

constructor(
  private http: HttpClient
) { }

getUserRecipes(email: string)
{
  return this.http.get(this.baseUrl + "/" + email);
}

getRecipeDrugs(recipeId: number){
  return this.http.get(this.baseUrl + "/drugs/" + recipeId);
}

addRecipe(recipe: any){
  return this.http.post(this.baseUrl, recipe);
}






}

