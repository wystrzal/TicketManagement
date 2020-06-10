import { Component, OnInit } from "@angular/core";
import { IssueService } from "../issue.service";
import { ErrorService } from "src/app/core/helpers/error.service";
import { IssueModel } from "src/app/models/issue.model";
import { ActivatedRoute } from "@angular/router";
import { Status } from "src/app/models/enums/status.enum";

@Component({
  selector: "app-issue-detail",
  templateUrl: "./issue-detail.component.html",
  styleUrls: ["./issue-detail.component.scss"],
})
export class IssueDetailComponent implements OnInit {
  issue: IssueModel;
  id: number;

  constructor(
    private issueService: IssueService,
    private errorSerivce: ErrorService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.params.subscribe((param) => {
      this.id = param["id"];
    });

    this.getIssue(this.id);
  }

  getIssue(id: number) {
    this.issueService.getIssue(id).subscribe(
      (data) => {
        this.issue = data;
      },
      (error) => {
        this.errorSerivce.newError(error);
      }
    );
  }

  changeIssueStatus(status: Status) {
    console.log(status);
    this.issueService.changeIssueStatus(this.issue.id, status).subscribe(
      () => {
        this.issue.status = Status[status];
      },
      (error) => {
        this.errorSerivce.newError(error);
      }
    );
  }
}
