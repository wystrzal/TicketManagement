import { Component, OnInit } from "@angular/core";
import { IssueService } from "../issue.service";
import { ErrorService } from "src/app/core/helpers/error.service";
import { IssueModel } from "src/app/models/issue.model";
import { ActivatedRoute } from "@angular/router";
import { Status } from "src/app/models/enums/status.enum";
import { IssueSupportModel } from "src/app/models/issueSupport.model";
import { AuthService } from "src/app/core/auth.service";

@Component({
  selector: "app-issue-detail",
  templateUrl: "./issue-detail.component.html",
  styleUrls: ["./issue-detail.component.scss"],
})
export class IssueDetailComponent implements OnInit {
  showAssign = true;
  issueSupport: IssueSupportModel[];
  currentUser: string;
  issue: IssueModel;
  id: number;

  constructor(
    private issueService: IssueService,
    private authService: AuthService,
    private errorSerivce: ErrorService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.route.params.subscribe((param) => {
      this.id = param["id"];
    });

    this.currentUser = this.authService.decodedToken.nameid;

    this.getIssue(this.id);
  }

  getIssue(id: number) {
    this.issueService.getIssue(id).subscribe(
      (data) => {
        this.issue = data;
        this.getIssueSupport();
      },
      (error) => {
        this.errorSerivce.newError(error);
      }
    );
  }

  changeIssueStatus(status: Status) {
    this.issueService.changeIssueStatus(this.issue.id, status).subscribe(
      () => {
        this.issue.status = Status[status];
      },
      (error) => {
        this.errorSerivce.newError(error);
      }
    );
  }

  assignToIssue(status: string) {
    this.issueService.assignToIssue(this.issue.id, this.currentUser).subscribe(
      () => {
        this.showAssign = false;
        this.issueSupport.push({ supportName: "Assigned", supportId: "0" });

        if (Status[status] == 1) {
          this.changeIssueStatus(Status.Open);
        }
      },
      (error) => {
        this.errorSerivce.newError(error);
      }
    );
  }

  getIssueSupport() {
    this.issueService.getIssueSupport(this.issue.id).subscribe(
      (data) => {
        data.forEach((element) => {
          if (element.supportId == this.currentUser) {
            return (this.showAssign = false);
          }
        });
        this.issueSupport = data;
      },
      (error) => {
        this.errorSerivce.newError(error);
      }
    );
  }
}
