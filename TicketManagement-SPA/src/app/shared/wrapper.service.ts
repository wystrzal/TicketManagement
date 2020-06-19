import { Injectable } from "@angular/core";
import { AuthService } from "../core/auth.service";
import { ErrorService } from "../core/helpers/error.service";
import { IssueService } from "../issues/issue.service";
import { DepartamentService } from "../admin-panel/new-user/departament.service";
import { IssueMessageService } from "../issues/issue-detail/issue-message/issue-message.service";

@Injectable({
  providedIn: "root",
})
export class WrapperService {
  constructor(
    private authService: AuthService,
    private errorService: ErrorService,
    private issueService: IssueService,
    private departamentService: DepartamentService,
    private issueMessageService: IssueMessageService
  ) {}

  public get IssueMessageService(): IssueMessageService {
    return this.issueMessageService;
  }

  public get ErrorService(): ErrorService {
    return this.errorService;
  }

  public get DepartamentService(): DepartamentService {
    return this.departamentService;
  }

  public get IssueService(): IssueService {
    return this.issueService;
  }

  public get AuthService(): AuthService {
    return this.authService;
  }
}
