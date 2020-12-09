/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { UserExistsComponent } from './user-exists.component';

describe('UserExistsComponent', () => {
  let component: UserExistsComponent;
  let fixture: ComponentFixture<UserExistsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserExistsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserExistsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
