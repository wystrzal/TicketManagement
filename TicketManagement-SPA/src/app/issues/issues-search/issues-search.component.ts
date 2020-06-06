import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { DepartamentModel } from "src/app/models/departament.model";
import { IssueService } from "../issue.service";
import { ErrorService } from "src/app/core/helpers/error.service";

@Component({
  selector: "app-issues-search",
  templateUrl: "./issues-search.component.html",
  styleUrls: ["./issues-search.component.scss"],
})
export class IssuesSearchComponent implements OnInit {
  @Output() startSearch = new EventEmitter();
  searchModel: any = { status: null, departament: null };
  departaments: DepartamentModel[];

  constructor(
    private issueService: IssueService,
    private errorService: ErrorService
  ) {}

  ngOnInit() {
    this.getDepartaments();
  }

  getDepartaments() {
    this.issueService.getIssueDepartaments().subscribe(
      (data) => {
        this.departaments = data;
      },
      (error) => {
        this.errorService.newError(error);
      }
    );
  }

  search() {
    this.searchModel.pageIndex = 1;
    this.startSearch.emit(this.searchModel);
  }

  reset() {
    this.searchModel = { status: null, departament: null };
    this.search();
  }
}
