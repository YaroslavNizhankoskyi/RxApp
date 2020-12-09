import { DrugService } from 'src/app/_services/drug.service';
import { DrugRecipeComponent } from './../../drug/drug/drug-recipe/drug-recipe/drug-recipe.component';
import { UserExistsComponent } from './../../user-exists/user-exists/user-exists.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { RecipeService } from './../../_services/recipe.service';
import { Component, OnInit } from '@angular/core';
import { Toast, ToastrService } from 'ngx-toastr';
import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-medic-general',
  templateUrl: './medic-general.component.html',
  styleUrls: ['./medic-general.component.scss']
})
export class MedicGeneralComponent implements OnInit {



  constructor(
    private recipeService: RecipeService,
    private toast: ToastrService,
    public modalService: NgbModal,
    private drugService: DrugService
  ) { }

  addRecipeMode = false;
  medicRecipes: any;
  drugs = new Array<any>();
  userEmail: string;
  medicEmail = localStorage.getItem('email');

  recipe = {
    medicId:  "",
    patientEmail: "",
    recipeDrugs: this.drugs
  }


  ngOnInit() {

  }

  createRecipe(){
    this.recipe.medicId = localStorage.getItem('userid');
    this.recipe.patientEmail = this.userEmail;
    this.recipe.recipeDrugs = this.drugs;

    this.recipeService.addRecipe(this.recipe).subscribe(res => 
      {
        this.toast.success('Added');
      }, err => {
        this.toast.error('Not added');
      } )
  }

  drop(event: CdkDragDrop<string[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(event.previousContainer.data,
                        event.container.data,
                        event.previousIndex,
                        event.currentIndex);
    }
  }

  onAddDrugToRecipe(drug: any)
  {
    const ref = this.modalService.open(DrugRecipeComponent,
      { centered: true, size: 'lg' });

    ref.result.then((result: any) => {
      if (result) {
        result.drugId = drug.id;
        result.name = drug.nameEng;
        this.drugs.push(result);
      }
    });
  }

  addRecipeToggle()
  {
    const ref = this.modalService.open(UserExistsComponent,
      { centered: true, size: 'lg' });

    ref.result.then((result: string) => {
      if (result) {
        this.addRecipeMode = true;
        this.userEmail = result;
      }
    });
  }

  recipeDrugRemove(recipeid: string){
    let index = this.drugs.findIndex(d => d.name === recipeid); //find index in your array
    this.drugs.splice(index, 1);
  }


  markAllIncompatible()
  {  
    this.drugs.forEach(d => {
      d.incompatible = false;
    })
    const selectedIds = this.drugs.map(({ drugId }) => drugId);
    console.log(selectedIds);
    this.drugService.markIncompatibleDrugs(selectedIds).subscribe(
      res => {
        let obj = res.toString()
        let array = obj.split(',');
        console.log(array)
        array.forEach(id => {
          let index = this.drugs.findIndex(item => item.drugId == parseInt(id))
          let drug = this.drugs[index];
          drug.incompatible = true;
        })
        console.log(this.drugs);
        
      }
      
    );
  }

  markAllAllergic()
  {  

    this.drugs.forEach(d => {
      d.incompatible = false;
    });

    const selectedIds = this.drugs.map(({ drugId }) => drugId);
    console.log(selectedIds);
    this.drugService.markAllergicDrugs(this.userEmail, selectedIds).subscribe(
      res => {
        let obj = res.toString()
        let array = obj.split(',');
        console.log(array)
        array.forEach(id => {
          let index = this.drugs.findIndex(item => item.drugId == parseInt(id))
          let drug = this.drugs[index];
          drug.incompatible = true;
        })
        console.log(this.drugs);
        
      }
      
    );
    // const selectedIds = this.drugs.map(({ drugId }) => drugId);
    // console.log(selectedIds);
    // this.drugService.markIncompatibleDrugs(selectedIds).subscribe(
    //   res => {
    //     let obj = res.toString()
    //     let array = obj.split(',');
    //     console.log(array)
    //     array.forEach(id => {
    //       let index = this.drugs.findIndex(item => item.drugId == parseInt(id))
    //       let drug = this.drugs[index];
    //       drug.incompatible = true;
    //     })
    //     console.log(this.drugs);
        
    //   }
      
    // );
  }
}
