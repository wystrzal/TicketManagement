import { Component, OnInit, Input } from "@angular/core";
import { IssueModel } from "src/app/models/issue.model";

@Component({
  selector: "app-issue-message",
  templateUrl: "./issue-message.component.html",
  styleUrls: ["./issue-message.component.scss"],
})
export class IssueMessageComponent implements OnInit {
  @Input() issue: IssueModel;

  constructor() {}

  ngOnInit() {}
}
