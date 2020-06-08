/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { IssueMessageService } from './issue-message.service';

describe('Service: IssueMessage', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [IssueMessageService]
    });
  });

  it('should ...', inject([IssueMessageService], (service: IssueMessageService) => {
    expect(service).toBeTruthy();
  }));
});
