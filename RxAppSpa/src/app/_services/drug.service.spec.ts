/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DrugService } from './drug.service';

describe('Service: Drug', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DrugService]
    });
  });

  it('should ...', inject([DrugService], (service: DrugService) => {
    expect(service).toBeTruthy();
  }));
});
