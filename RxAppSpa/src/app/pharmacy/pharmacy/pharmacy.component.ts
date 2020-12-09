import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { UserExistsComponent } from 'src/app/user-exists/user-exists/user-exists.component';
import { DrugService } from 'src/app/_services/drug.service';
import { RecipeService } from 'src/app/_services/recipe.service';

@Component({
  selector: 'app-pharmacy',
  templateUrl: './pharmacy.component.html',
  styleUrls: ['./pharmacy.component.scss']
})
export class PharmacyComponent implements OnInit {

  viewRecipeMode = false;
  userEmail = "";

  constructor(
    private recipeService: RecipeService,
    private toast: ToastrService,
    public modalService: NgbModal,
    private drugService: DrugService
  ) { }

  ngOnInit() {
  }

  viewRecipeToggle()
  {
    const ref = this.modalService.open(UserExistsComponent,
      { centered: true, size: 'lg' });

    ref.result.then((result: string) => {
      if (result) {
        this.viewRecipeMode = true;
        this.userEmail = result;
      }
    });
  }
}
