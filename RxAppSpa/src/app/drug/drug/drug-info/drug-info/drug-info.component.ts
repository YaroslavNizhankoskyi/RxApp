import { DrugService } from 'src/app/_services/drug.service';
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Drug } from 'src/app/_models/Drug';

@Component({
  selector: 'app-drug-info',
  templateUrl: './drug-info.component.html',
  styleUrls: ['./drug-info.component.scss']
})
export class DrugInfoComponent implements OnInit {

  selectedDrug: any;
  ingredients: any;


  constructor(public modal: NgbActiveModal,
    public drugService: DrugService) { }

  ngOnInit() {
    console.log(this.selectedDrug.id);
    this.drugService.getDrugIngredients(this.selectedDrug.id).subscribe(
      res => {
        console.log(res);
        this.ingredients = res;
        console.log(this.ingredients);
      }, err => {

      }
    );
  }



}
