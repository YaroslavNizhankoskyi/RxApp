import { TestBed } from '@angular/core/testing';

import { MedicGuard } from './medic.guard';

describe('MedicGuard', () => {
  let guard: MedicGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(MedicGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
