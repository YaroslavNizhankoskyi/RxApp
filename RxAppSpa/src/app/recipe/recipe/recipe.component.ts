import { Component, Input, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { RecipeService } from 'src/app/_services/recipe.service';
import { RecipeInfoComponent } from '../recipe-component/recipe-info/recipe-info/recipe-info.component';

@Component({
  selector: 'app-recipe',
  templateUrl: './recipe.component.html',
  styleUrls: ['./recipe.component.scss']
})
export class RecipeComponent implements OnInit {

  @Input() userEmail: string;
  constructor(
    private modalService: NgbModal,
    private recipeService: RecipeService,
    private toast: ToastrService
  ) { }

  recipes: any;

  ngOnInit() {
    this.recipeService.getUserRecipes(this.userEmail).subscribe(
      res => {
        this.recipes = res;
      }, 
      err => {
        this.toast.error("No recipes");
      }
    )
  }
  recipeDrugs: any;

  recipeDetails(recipeId: number){


    this.recipeService.getRecipeDrugs(recipeId).subscribe(
      res => {
        console.log(res)
        this.recipeDrugs = res;
      }, 
      err => {
        this.toast.error("No recipe drugs");
      }
    )

    console.log(this.recipeDrugs);
    const ref = this.modalService.open(RecipeInfoComponent,
      { centered: true, size: 'lg' });
    ref.componentInstance.recipeDrugs = this.recipeDrugs;
  }
}
