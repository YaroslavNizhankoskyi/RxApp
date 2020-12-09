import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-recipe-info',
  templateUrl: './recipe-info.component.html',
  styleUrls: ['./recipe-info.component.scss']
})
export class RecipeInfoComponent implements OnInit {

  constructor(
    public modal: NgbActiveModal
  ) { }
  recipeDrugs: any;

  ngOnInit() {
  }

}
