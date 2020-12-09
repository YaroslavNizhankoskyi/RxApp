import { FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-drug-recipe',
  templateUrl: './drug-recipe.component.html',
  styleUrls: ['./drug-recipe.component.scss']
})
export class DrugRecipeComponent implements OnInit {

  constructor(
    public modal: NgbActiveModal,
    private toast: ToastrService,
    private authService: AuthService,
    private fb: FormBuilder ) { }


    recipe = {
        perDay: 18,
        dose: "q2eqe",
        comment: "qeq2e",
        name: "",
        id: 0,
        incompatible: false,
        allergic: false
    };
  
    recipeDrugForm = this.fb.group({
      perDay: [''],
      dose: [''],
      comment: ['', Validators.maxLength(50)],
    });

  ngOnInit() {
  }
  assign(){
    this.recipe.comment = this.recipeDrugForm.controls['comment'].value; 
    this.recipe.dose = this.recipeDrugForm.controls['dose'].value;
    this.recipe.perDay = this.recipeDrugForm.controls['perDay'].value;
    this.modal.close(this.recipe);
  }

}
