import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { ErrorService } from "src/app/core/helpers/error.service";
import { IssueService } from "../issue.service";
import { SearchSpecificationModel } from "src/app/models/searchSpecification.model";
import { SearchFor } from "src/app/models/enums/searchFor.enum";
import { PaginatedItemsModel } from "src/app/models/paginatedItems.model";

@Injectable()
export class AllIssuesResolver implements Resolve<PaginatedItemsModel> {
  searchSpec: SearchSpecificationModel = {
    departament: null,
    declarantLastName: null,
    status: null,
    title: null,
    userId: null,
    pageIndex: null,
    pageSize: null,
    searchFor: SearchFor.AllIssues,
  };

  constructor(
    private issueService: IssueService,
    private errorService: ErrorService
  ) {}

  resolve(route: ActivatedRouteSnapshot): Observable<PaginatedItemsModel> {
    return this.issueService.getIssues(this.searchSpec).pipe(
      catchError((error) => {
        this.errorService.newError(error);
        return of(null);
      })
    );
  }
}
