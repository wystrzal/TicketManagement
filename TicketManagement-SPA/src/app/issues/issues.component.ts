import { Component, OnInit } from "@angular/core";
import { SearchSpecificationModel } from "src/app/models/searchSpecification.model";
import { IssueService } from "./issue.service";
import { IssueModel } from "src/app/models/issue.model";
import { ActivatedRoute } from "@angular/router";
import { SearchFor } from "src/app/models/enums/searchFor.enum";
import { PaginatedItemsModel } from "../models/paginatedItems.model";

@Component({
  selector: "app-issues",
  templateUrl: "./issues.component.html",
  styleUrls: ["./issues.component.scss"],
})
export class IssuesComponent implements OnInit {
  paginatedIssues: PaginatedItemsModel;
  searchFor: SearchFor;

  constructor(
    private issueService: IssueService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.paginatedIssues = this.route.snapshot.data.issues;
    this.searchFor = this.route.snapshot.data.searchFor;
  }

  search(searchModel: any) {
    console.log(searchModel);
  }
}
