import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-admin-role',
  templateUrl: './admin-role.component.html',
  styleUrls: ['./admin-role.component.scss']
})
export class AdminRoleComponent implements OnInit {

  constructor(
    private toast: ToastrService,
    private adminService: AdminService,
    private fb: FormBuilder
  ) { }

  administrators: any;
  medics: any;
  pharmacists: any;


  editUsersInRole(){
    this.adminService.editUsersInRole('Medic', this.medics).subscribe(
      res => {
      });
    this.adminService.editUsersInRole('Admin', this.administrators).subscribe(
      res => {
      });
    this.adminService.editUsersInRole('Pharmacist', this.pharmacists).subscribe(
      res => {
      });
  }

  ngOnInit() {
    this.adminService.getUsersInRole('Medic').subscribe(
      res => {
        this.medics = res;
      });

    this.adminService.getUsersInRole('Admin').subscribe(
      res => {
        this.administrators = res;
      })

    this.adminService.getUsersInRole('Pharmacist').subscribe(
      res => {
        this.pharmacists = res;
      })
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

}
