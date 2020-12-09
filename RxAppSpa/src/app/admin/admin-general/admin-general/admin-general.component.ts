import { FormBuilder, FormControl } from '@angular/forms';
import { AdminService } from './../../../_services/admin.service';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-admin-general',
  templateUrl: './admin-general.component.html',
  styleUrls: ['./admin-general.component.scss']
})
export class AdminGeneralComponent implements OnInit {

  pharmGroups: any;

  ingredients: any;

  incompatible: any;



  constructor(
    private toast: ToastrService,
    private adminService: AdminService,
    private fb: FormBuilder
  ) { }

  incompatibleForm = this.fb.group({
    firstid: [''],
    secondid: [''],
    incompatibleId: ['']
  })

  ingredientForm = this.fb.group({
    id: [''],
    edit: ['']
  });


  PGForm = this.fb.group({
    id: [''],
    edit: ['']
  });


  removeIngredient(){
    const id = this.ingredientForm.controls['id'].value;
    if(id !== null ){
        this.adminService.deleteIngredient(+id).subscribe(res => {
          this.toast.success("Removed");
        }, error => {
          this.toast.error("Error");
        });
    }else{
      this.toast.error("Incorrect data");
    }
  }

  editIngredient(){
    const name = this.ingredientForm.controls['edit'].value;
    const id = this.ingredientForm.controls['id'].value;
    if(name !== null && id !== null){
      this.adminService.editIngredient(+id, name).subscribe(res => {
        this.toast.success("Edited");
      }, err => {
        this.toast.error("Error")
      } );
    }else{
      this.toast.error("Incorrect data");
    }
  }

  addIngredient(){
    const name = this.ingredientForm.controls['edit'].value
    if(name !== null){
      this.adminService.createIngredient(name).subscribe(res => {
        this.toast.success("Created");
      }, err => {
        this.toast.error("Error")
      });
    }else{
      this.toast.error("Incorrect data");
    }
  }

  removePG(){
    const id = this.PGForm.controls['id'].value;
    if(id !== null ){
        this.adminService.deletePG(+id).subscribe(res => {
          this.toast.success("Removed");
        }, error => {
          this.toast.error("Error");
        });
    }else{
      this.toast.error("Incorrect data");
    }
  }

  editPG(){
    const name = this.PGForm.controls['edit'].value;
    const id = this.PGForm.controls['id'].value;
    if(name !== null && id !== null){
      this.adminService.editPG(+id, name).subscribe(res => {
        this.toast.success("Edited");
      }, err => {
        this.toast.error("Error")
      } );
    }else{
      this.toast.error("Incorrect data");
    }
  }

  addPG(){
    const name = this.PGForm.controls['edit'].value
    if(name !== null){
      this.adminService.createPG(name).subscribe(res => {
        this.toast.success("Created");
      }, err => {
        this.toast.error("Error")
      });
    }else{
      this.toast.error("Incorrect data");
    }
  }

  removeIncompatible(){
    const id = this.incompatibleForm.controls['incompatibleId'].value;
    if(id !== null ){
        this.adminService.deleteIncompatible(+id).subscribe(res => {
          this.toast.success("Removed");
        }, error => {
          this.toast.error("Error");
        });
    }else{
      this.toast.error("Incorrect data");
    }
  }

  addIncompatible(){
    const firstid = this.incompatibleForm.controls['firstid'].value;
    const secondid = this.incompatibleForm.controls['secondid'].value;
    if(firstid !== null && secondid !== null && firstid !== secondid){
      this.adminService.createIncompatible(+firstid, +secondid).subscribe(res => {
        this.toast.success("Created");
      }, err => {
        this.toast.error("Error")
      });
    }else{
      this.toast.error("Incorrect data");
    }
  }

  ngOnInit() {
    this.adminService.getAllPG().subscribe(res => {
      console.log(res);
      this.pharmGroups = res;
      console.log(this.pharmGroups) 
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

    this.adminService.getAllIncompatible().subscribe(res => {
      this.incompatible = res;
      console.log(res); 
    }, error => {
      this.toast.error(error);
    }); 
  }

}
