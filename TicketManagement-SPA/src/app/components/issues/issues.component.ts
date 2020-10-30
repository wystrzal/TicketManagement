import { Component, OnInit } from "@angular/core";
import { SearchSpecificationModel } from "src/app/models/searchSpecification.model";
import { ActivatedRoute } from "@angular/router";
import { PaginatedItemsModel } from "src/app/models/paginatedItems.model";
import { WrapperService } from "src/app/shared/wrapper.service";

@Component({
  selector: "app-issues",
  templateUrl: "./issues.component.html",
  styleUrls: ["./issues.component.scss"],
})
export class IssuesComponent implements OnInit {
  paginatedIssues: PaginatedItemsModel;
  currentPage: number;
  searchSpec: SearchSpecificationModel;

  constructor(
    private wrapperService: WrapperService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.paginatedIssues = this.route.snapshot.data.issues;
  }

  search(searchModel: any) {
    this.SetSearchSpecification(searchModel);

    this.wrapperService.IssueService.getIssues(this.searchSpec).subscribe(
      (data) => {
        this.paginatedIssues = data;
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );

    this.currentPage = 1;
  }

  private SetSearchSpecification(searchModel: any) {
    this.searchSpec = searchModel;
    this.searchSpec.searchFor = this.route.snapshot.data.searchFor;
    this.searchSpec.userId = this.wrapperService.AuthService.decodedToken.nameid;
  }

  changePage(pageIndex) {
    this.searchSpec.pageIndex = pageIndex.page;
    this.search(this.searchSpec);
  }
}
