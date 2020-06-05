import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { SearchSpecificationModel } from "src/app/models/searchSpecification.model";
import { Observable } from "rxjs";
import { IssueModel } from "src/app/models/issue.model";
import { PaginatedItemsModel } from "../models/paginatedItems.model";
import { map } from "rxjs/operators";
import { Status } from "../models/enums/status.enum";

@Injectable({
  providedIn: "root",
})
export class IssueService {
  baseUrl = environment.apiUrl + "issue/";

  constructor(private http: HttpClient) {}

  getIssues(
    searchSpecification: SearchSpecificationModel
  ): Observable<PaginatedItemsModel> {
    let params = new HttpParams();

    if (searchSpecification.declarantLastName != null) {
      params = params.append(
        "declarantLastName",
        searchSpecification.declarantLastName
      );
    }

    if (searchSpecification.departament != null) {
      params = params.append("departament", searchSpecification.departament);
    }

    if (searchSpecification.status != null) {
      params = params.append("status", searchSpecification.status.toString());
    }

    if (searchSpecification.title != null) {
      params = params.append("title", searchSpecification.title);
    }

    if (searchSpecification.userId != null) {
      params = params.append("userId", searchSpecification.userId);
    }

    if (searchSpecification.pageIndex != null) {
      params = params.append(
        "pageIndex",
        searchSpecification.pageIndex.toString()
      );
    }

    if (searchSpecification.pageSize != null) {
      params = params.append(
        "pageSize",
        searchSpecification.pageSize.toString()
      );
    }

    if (searchSpecification.searchFor != null) {
      params = params.append(
        "searchFor",
        searchSpecification.searchFor.toString()
      );
    }

    return this.http
      .get<PaginatedItemsModel>(this.baseUrl, { params })
      .pipe(
        map((paginatedItems: PaginatedItemsModel) => {
          paginatedItems.data.forEach((element) => {
            element.status = Status[element.status];
          });

          return paginatedItems;
        })
      );
  }
}
