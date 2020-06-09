import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "src/app/core/auth.service";
import { IssueService } from "../issue.service";
import { ErrorService } from "src/app/core/helpers/error.service";

@Component({
  selector: "app-new-issue",
  templateUrl: "./new-issue.component.html",
  styleUrls: ["./new-issue.component.scss"],
})
export class NewIssueComponent implements OnInit {
  issueModel: any = {};

  constructor(
    private router: Router,
    private authService: AuthService,
    private issueService: IssueService,
    private errorService: ErrorService
  ) {}

  ngOnInit() {}

  addNewIssue() {
    this.issueModel.declarantId = this.authService.decodedToken.nameid;

    this.issueService.addNewIssue(this.issueModel).subscribe(
      () => {
        this.router.navigate(["user/issues"]);
      },
      (error) => {
        this.errorService.newError(error);
      }
    );
  }
}
