import { Injectable } from "@angular/core";
import { Resolve, ActivatedRouteSnapshot } from "@angular/router";
import { Observable, of } from "rxjs";
import { catchError } from "rxjs/operators";
import { SearchSpecificationModel } from "src/app/models/searchSpecification.model";
import { SearchFor } from "src/app/models/enums/searchFor.enum";
import { PaginatedItemsModel } from "src/app/models/paginatedItems.model";
import { WrapperService } from "src/app/shared/wrapper.service";

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

  constructor(private wrapperService: WrapperService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<PaginatedItemsModel> {
    return this.wrapperService.IssueService.getIssues(this.searchSpec).pipe(
      catchError((error) => {
        this.wrapperService.ErrorService.newError(error);
        return of(null);
      })
    );
  }
}
