import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/_services/admin.service';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DrugService } from 'src/app/_services/drug.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-drug-create',
  templateUrl: './drug-create.component.html',
  styleUrls: ['./drug-create.component.scss']
})
export class DrugCreateComponent implements OnInit {


  drug = this.fb.group({
    ingredients:[],
    pharmGroupId:[''],
    nameEng: ['', Validators.maxLength(50)],
    nameRus: ['', Validators.maxLength(50)],
    action: ['', Validators.maxLength(700)],
    dosing: ['', Validators.maxLength(700)],
    overdose:['', Validators.maxLength(700)],
    storageCondition:['', Validators.maxLength(700)],
    nozology:['', Validators.maxLength(700)],
    packaging:['', Validators.maxLength(700)],
    indications:['', Validators.maxLength(700)],
    bestBefore:['', Validators.maxLength(700)],
    specialCases:['', Validators.maxLength(700)],
    sideEffects:['', Validators.maxLength(700)],
    duringPregnancy:['', Validators.maxLength(700)],
    pharmacodynamics:['', Validators.maxLength(700)],
    pharmacokinetics:['', Validators.maxLength(700)]
  });


  ingredients:any;
  pharmgroups:any;


  constructor(
    private fb: FormBuilder,
    private drugService: DrugService,
    public modal: NgbActiveModal,
    private adminService: AdminService,
    private toast: ToastrService) { }

  ngOnInit() {
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


  createDrug(){
    console.log(this.drug.value);
    const createDrug = Object.assign({}, this.drug.value);
    this.drugService.createDrug(createDrug).subscribe(
      res => {
        this.toast.success("Created");
      },
      err => {
        this.toast.error("Error while creating drug");
      }
    )
    this.modal.close();
  }
}
