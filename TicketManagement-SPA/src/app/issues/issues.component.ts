import { Component, OnInit } from "@angular/core";
import { SearchSpecificationModel } from "src/app/models/searchSpecification.model";
import { IssueService } from "./issue.service";
import { ActivatedRoute } from "@angular/router";
import { PaginatedItemsModel } from "../models/paginatedItems.model";
import { ErrorService } from "../core/helpers/error.service";
import { Status } from "../models/enums/status.enum";
import { AuthService } from "../core/auth.service";

@Component({
  selector: "app-issues",
  templateUrl: "./issues.component.html",
  styleUrls: ["./issues.component.scss"],
})
export class IssuesComponent implements OnInit {
  paginatedIssues: PaginatedItemsModel;
  currentPage: number;
  searchSpec: SearchSpecificationModel = {
    departament: null,
    declarantLastName: null,
    status: null,
    title: null,
    userId: null,
    pageIndex: null,
    pageSize: null,
    searchFor: null,
  };

  constructor(
    private issueService: IssueService,
    private route: ActivatedRoute,
    private errorService: ErrorService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.paginatedIssues = this.route.snapshot.data.issues;
  }

  search(searchModel: any) {
    this.searchSpec = searchModel;
    this.searchSpec.searchFor = this.route.snapshot.data.searchFor;
    this.searchSpec.userId = this.authService.decodedToken.nameid;

    this.issueService.getIssues(this.searchSpec).subscribe(
      (data) => {
        this.paginatedIssues = data;
      },
      (error) => {
        this.errorService.newError(error);
      }
    );

    this.currentPage = 1;
  }

  changePage(pageIndex) {
    this.searchSpec.pageIndex = pageIndex.page;

    this.search(this.searchSpec);
  }
}
