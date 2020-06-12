/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DepartamentService } from './departament.service';

describe('Service: Departament', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DepartamentService]
    });
  });

  it('should ...', inject([DepartamentService], (service: DepartamentService) => {
    expect(service).toBeTruthy();
  }));
});
