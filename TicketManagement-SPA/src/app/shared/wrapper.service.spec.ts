/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { WrapperService } from './wrapper.service';

describe('Service: Wrapper', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WrapperService]
    });
  });

  it('should ...', inject([WrapperService], (service: WrapperService) => {
    expect(service).toBeTruthy();
  }));
});
