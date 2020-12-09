import { AuthService } from 'src/app/_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-exists',
  templateUrl: './user-exists.component.html',
  styleUrls: ['./user-exists.component.scss']
})
export class UserExistsComponent implements OnInit {

  constructor(
    public modal: NgbActiveModal,
    private toast: ToastrService,
    private authService: AuthService 
  ) { }

  ngOnInit() {
  }
  allowed = false;

  email: string;

  checkUserAllowed(){
    this.authService.getAbilityAddRecipes(this.email)
    .subscribe(res => {
      if(res){
        this.allowed = true;
      }else{
        this.toast.error("User doesn't allow to add recipes");
      }
    }, 
    err => {
      this.toast.error("No user with such email")
    })
  }

}
