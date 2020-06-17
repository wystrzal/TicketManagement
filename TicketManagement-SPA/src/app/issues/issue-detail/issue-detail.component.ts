import { Component, OnInit } from "@angular/core";
import { IssueService } from "../issue.service";
import { ErrorService } from "src/app/core/helpers/error.service";
import { IssueModel } from "src/app/models/issue.model";
import { ActivatedRoute, Router } from "@angular/router";
import { Status } from "src/app/models/enums/status.enum";
import { IssueSupportModel } from "src/app/models/issueSupport.model";
import { AuthService } from "src/app/core/auth.service";
import { Priority } from "src/app/models/enums/priority.enum";
import { Location } from "@angular/common";
import { UserModel } from "src/app/models/user.model";

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
  supportToAssign: UserModel[];

  constructor(
    private issueService: IssueService,
    private authService: AuthService,
    private errorSerivce: ErrorService,
    private route: ActivatedRoute,
    private location: Location
  ) {}

  ngOnInit() {
    this.route.params.subscribe((param) => {
      this.id = param["id"];
    });

    this.currentUser = this.authService.decodedToken.nameid;

    this.getIssue(this.id);
    this.getSupport();
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

  getSupport() {
    this.authService.getUsers("support").subscribe(
      (data) => {
        this.supportToAssign = data;
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

  changeIssuePriority(priority: Priority) {
    this.issueService.changeIssuePriority(this.issue.id, priority).subscribe(
      () => {
        this.issue.priority = Priority[priority];
      },
      (error) => {
        this.errorSerivce.newError(error);
      }
    );
  }

  deleteIssue(issueId: number) {
    this.issueService.deleteIssue(issueId).subscribe(
      () => {
        this.location.back();
      },
      (error) => {
        this.errorSerivce.newError(error);
      }
    );
  }

  assignToIssue(status: string, userId: string) {
    debugger;
    if (userId == null) {
      userId = this.currentUser;
    } else {
      if (this.issue.assignedSupport.indexOf(userId) !== -1) {
        return;
      }
    }

    this.issueService.assignToIssue(this.issue.id, userId).subscribe(
      () => {
        this.showAssign = false;
        this.issueSupport.push({ supportName: "Assigned", supportId: "0" });
        this.issue.assignedSupport.push(userId);

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
