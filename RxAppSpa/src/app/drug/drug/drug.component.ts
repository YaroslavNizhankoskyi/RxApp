import { Router } from '@angular/router';
import { NgbActiveModal, NgbModal, NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastrModule, ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/_services/admin.service';
import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { Drug } from 'src/app/_models/Drug';
import { DrugParams } from 'src/app/_models/DrugParams';
import { Pagination } from 'src/app/_models/pagination';
import { DrugService } from 'src/app/_services/drug.service';
import { DrugInfoComponent } from './drug-info/drug-info/drug-info.component';
import { DrugEditComponent } from './drug-edit/drug-edit/drug-edit.component';
import { DrugCreateComponent } from './drug-create/drug-create/drug-create.component';

@Component({
  selector: 'app-drug',
  templateUrl: './drug.component.html',
  styleUrls: ['./drug.component.scss']
})
export class DrugComponent implements OnInit {

  @Output() drugSelected = new EventEmitter<any>();
  role = localStorage.getItem('role');
  drugs: any[];
  pagination: Pagination = {
    currentPage: 0,
    totalItems: 0,
    totalPages: 0,
    itemsPerPage: 0
  };
  drugParams: DrugParams;
  user: Drug;
  pharmgroups: any;



  constructor(private drugService: DrugService,
    private adminService: AdminService,
    private toast: ToastrService,
    private modalService: NgbModal
    ) {
    this.drugParams = this.drugService.getDrugParams();
    this.drugs = new Array();

    this.adminService.getAllPG().subscribe(res => {
      this.pharmgroups = res; 
    }, error => {
      this.toast.error(error);
      console.log(error);
    });
  }

  ngOnInit(): void {
    this.loadDrugs();
  }

  checkboxOrder(){
    if(localStorage.getItem('order')){
      localStorage.removeItem('order')
    }else{
      localStorage.setItem('order', 'order')
    }
  }

  checkboxEng(){
    if(localStorage.getItem('eng')){
      localStorage.removeItem('eng')
    }else{
      localStorage.setItem('eng', 'eng')
    }
  }
  loadDrugs() {
    const eng = Boolean(localStorage.getItem('eng'));
    const order = Boolean(localStorage.getItem('order'));
    this.drugParams.alphabeticalOrderAsc = order;
    this.drugParams.eng = eng;
    this.drugService.setDrugParams(this.drugParams);
    this.drugService.getDrugs(this.drugParams).subscribe(
      res => {
        this.drugs = new Array();
        Object.assign(this.drugs, res.result);
        Object.assign(this.pagination, res.pagination)
      }
    )
  }


  drugInfo(drug: any) {
    // this.router.navigateByUrl(`EditUser/${userModel.id}`);
    const ref = this.modalService.open(DrugInfoComponent,
       { centered: true, size: 'lg' });
    ref.componentInstance.selectedDrug = drug;
  }

  drugEdit(drug: any){
    const ref = this.modalService.open(DrugEditComponent, { centered: true, size: 'lg' });
    ref.componentInstance.selectedDrug = drug;
    ref.componentInstance.drugId = drug.id;
    
  }

  drugCreate(){
    const ref = this.modalService.open(DrugCreateComponent, { centered: true, size: 'lg' });
  }

  drugRemove(id: any){
      this.drugService.removeDrug(id).subscribe(
        res => {
          this.toast.success("Drug removed");
        },
        err => {
          this.toast.error("Error while deleting drug");
        }
      )
  }

  addDrugToRecipe(drug: Drug){
    this.drugSelected.emit(drug);
  }


  resetFilters() {
    localStorage.removeItem('order');
    localStorage.removeItem('eng');
    this.drugParams = this.drugService.resetDrugParams();
    this.loadDrugs();
  }

  pageChanged(event: any) {
    this.drugParams.pageNumber = event.page;
    this.drugService.setDrugParams(this.drugParams);
    this.loadDrugs();
  }
}
