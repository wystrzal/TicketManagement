import { Component, OnInit } from "@angular/core";
import { IssueModel } from "src/app/models/issue.model";
import { ActivatedRoute } from "@angular/router";
import { Status } from "src/app/models/enums/status.enum";
import { IssueSupportModel } from "src/app/models/issueSupport.model";
import { Priority } from "src/app/models/enums/priority.enum";
import { Location } from "@angular/common";
import { UserModel } from "src/app/models/user.model";
import { WrapperService } from "src/app/shared/wrapper.service";

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
    private route: ActivatedRoute,
    private location: Location,
    private wrapperService: WrapperService
  ) {}

  ngOnInit() {
    this.route.params.subscribe((param) => {
      this.id = param["id"];
    });

    this.currentUser = this.wrapperService.AuthService.decodedToken.nameid;

    this.getIssue(this.id);
    this.getSupport();
  }

  getIssue(id: number) {
    this.wrapperService.IssueService.getIssue(id).subscribe(
      (data) => {
        this.issue = data;
        this.getIssueSupport();
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }

  getSupport() {
    this.wrapperService.AuthService.getUsers("support").subscribe(
      (data) => {
        this.supportToAssign = data;
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }

  changeIssueStatus(status: Status) {
    this.wrapperService.IssueService.changeIssueStatus(
      this.issue.id,
      status
    ).subscribe(
      () => {
        this.issue.status = Status[status];
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }

  changeIssuePriority(priority: Priority) {
    this.wrapperService.IssueService.changeIssuePriority(
      this.issue.id,
      priority
    ).subscribe(
      () => {
        this.issue.priority = Priority[priority];
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }

  deleteIssue(issueId: number) {
    this.wrapperService.IssueService.deleteIssue(issueId).subscribe(
      () => {
        this.location.back();
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }

  assignToIssue(status: string, supportId: string) {
    if (this.issue.assignedSupport.indexOf(supportId) !== -1) {
      return;
    }

    this.wrapperService.IssueService.assignToIssue(
      this.issue.id,
      supportId
    ).subscribe(
      () => {
        this.showAssign = false;
        this.issueSupport.push({
          supportName: "Assigned",
          supportId: supportId,
        });
        this.issue.assignedSupport.push(supportId);

        if (Status[status] == 1) {
          this.changeIssueStatus(Status.Open);
        }
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }

  unassignFromIssue(supportId: string, index: number) {
    this.wrapperService.IssueService.unassignFromIssue(
      this.issue.id,
      supportId
    ).subscribe(
      () => {
        this.issueSupport.splice(index, 1);
        this.issue.assignedSupport.splice(index, 1);

        if (supportId == this.currentUser) {
          this.showAssign = true;
        }
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }

  getIssueSupport() {
    this.wrapperService.IssueService.getIssueSupport(this.issue.id).subscribe(
      (data) => {
        data.forEach((element) => {
          if (element.supportId == this.currentUser) {
            return (this.showAssign = false);
          }
        });
        this.issueSupport = data;
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }
}
