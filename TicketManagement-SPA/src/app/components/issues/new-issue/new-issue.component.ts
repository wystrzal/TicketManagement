import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { WrapperService } from "src/app/shared/wrapper.service";

@Component({
  selector: "app-new-issue",
  templateUrl: "./new-issue.component.html",
  styleUrls: ["./new-issue.component.scss"],
})
export class NewIssueComponent implements OnInit {
  issueModel: any = {};

  constructor(private router: Router, private wrapperService: WrapperService) {}

  ngOnInit() {}

  addNewIssue() {
    this.issueModel.declarantId = this.wrapperService.AuthService.decodedToken.nameid;

    this.wrapperService.IssueService.addNewIssue(this.issueModel).subscribe(
      () => {
        this.router.navigate(["user/issues"]);
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }
}
