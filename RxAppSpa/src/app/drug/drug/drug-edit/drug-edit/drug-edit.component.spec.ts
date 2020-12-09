/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DrugEditComponent } from './drug-edit.component';

describe('DrugEditComponent', () => {
  let component: DrugEditComponent;
  let fixture: ComponentFixture<DrugEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DrugEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DrugEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
