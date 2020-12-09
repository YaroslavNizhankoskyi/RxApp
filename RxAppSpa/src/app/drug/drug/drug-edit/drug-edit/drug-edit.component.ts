import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs/operators';
import { AdminService } from 'src/app/_services/admin.service';
import { DrugService } from 'src/app/_services/drug.service';

@Component({
  selector: 'app-drug-edit',
  templateUrl: './drug-edit.component.html',
  styleUrls: ['./drug-edit.component.scss']
})
export class DrugEditComponent implements OnInit {

  drug: FormGroup;

  setForm(){
    this.drug = this.fb.group({
      ingredients:[this.ingredients],
      pharmGroupId:[this.selectedDrug.pharmGroupId],
      nameEng: [this.selectedDrug.nameEng, Validators.maxLength(50)],
      nameRus: [this.selectedDrug.nameRus, Validators.maxLength(50)],
      action: [this.selectedDrug.action, Validators.maxLength(700)],
      dosing: [this.selectedDrug.dosing, Validators.maxLength(700)],
      overdose: [this.selectedDrug.overdose, Validators.maxLength(700)],
      storageCondition: [this.selectedDrug.storageCondition, Validators.maxLength(700)],
      nozology:[this.selectedDrug.nozology, Validators.maxLength(700)],
      packaging:[this.selectedDrug.packaging, Validators.maxLength(700)],
      indications:[this.selectedDrug.indications, Validators.maxLength(700)],
      bestBefore:[this.selectedDrug.bestBefore, Validators.maxLength(700)],
      specialCases:[this.selectedDrug.specialCases, Validators.maxLength(700)],
      sideEffects:[this.selectedDrug.sideEffects, Validators.maxLength(700)],
      duringPregnancy:[this.selectedDrug.duringPregnancy, Validators.maxLength(700)],
      pharmacodynamics:[this.selectedDrug.pharmaocdynamics, Validators.maxLength(700)],
      pharmacokinetics:[this.selectedDrug.pharmacokinetics, Validators.maxLength(700)]
    });
  }

  drugId: any;
  ingredients:any;
  pharmgroups:any;
  selectedDrug: any;


  constructor(
    private fb: FormBuilder,
    private drugService: DrugService,
    public modal: NgbActiveModal,
    private adminService: AdminService,
    private toast: ToastrService) { }

  ngOnInit() {

    this.drugService.getDrugIngredients(this.drugId).pipe(
      map((response: any) => {
        console.log(response);
        this.ingredients = response;
      }));


    this.setForm();

    this.adminService.getAllPG().subscribe(res => {
      this.pharmgroups = res; 
    }, error => {
      this.toast.error(error);
      console.log(error);
    });

    this.adminService.getAllIngredients().subscribe(res => {
      this.ingredients = res; 
    }, error => {
      this.toast.error(error);
      console.log(error);
    });

  }


  editDrug(){
    const editedDrug = Object.assign({}, this.drug.value);

    this.drugService.editDrug(editedDrug, this.drugId).subscribe(
      res => {
        this.toast.success("Drug edited");
      },
      err => {
        this.toast.error("Error while editing drug");
      }
    )
  }

}
