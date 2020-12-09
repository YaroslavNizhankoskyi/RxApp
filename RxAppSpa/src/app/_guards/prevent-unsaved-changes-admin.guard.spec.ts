import { TestBed } from '@angular/core/testing';

import { PreventUnsavedChangesAdminGuard } from './prevent-unsaved-changes-admin.guard';

describe('PreventUnsavedChangesAdminGuard', () => {
  let guard: PreventUnsavedChangesAdminGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(PreventUnsavedChangesAdminGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
