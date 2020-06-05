import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { ErrorService } from "src/app/core/helpers/error.service";
import { IssueService } from "../issue.service";
import { SearchSpecificationModel } from "src/app/models/searchSpecification.model";
import { SearchFor } from "src/app/models/enums/searchFor.enum";
import { AuthService } from "src/app/core/auth.service";
import { PaginatedItemsModel } from "src/app/models/paginatedItems.model";

@Injectable()
export class SupportIssuesResolver implements Resolve<PaginatedItemsModel> {
  searchSpec: SearchSpecificationModel = {
    departament: null,
    declarantLastName: null,
    status: null,
    title: null,
    userId: null,
    pageIndex: null,
    pageSize: null,
    searchFor: SearchFor.SupportIssues,
  };

  constructor(
    private issueService: IssueService,
    private errorService: ErrorService,
    private authService: AuthService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<PaginatedItemsModel> {
    this.searchSpec.userId = this.authService.decodedToken.nameid;

    return this.issueService.getIssues(this.searchSpec).pipe(
      catchError((error) => {
        this.errorService.newError(error);
        return of(null);
      })
    );
  }
}
