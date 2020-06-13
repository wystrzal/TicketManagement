import { Component, OnInit, Input } from "@angular/core";
import { IssueModel } from "src/app/models/issue.model";
import { AuthService } from "src/app/core/auth.service";
import { IssueMessageService } from "./issue-message.service";
import { IssueMessageModel } from "src/app/models/issueMessage.model";
import { ErrorService } from "src/app/core/helpers/error.service";
import { IssueService } from "../../issue.service";
import { Status } from "src/app/models/enums/status.enum";

@Component({
  selector: "app-issue-message",
  templateUrl: "./issue-message.component.html",
  styleUrls: ["./issue-message.component.scss"],
})
export class IssueMessageComponent implements OnInit {
  @Input() issue: IssueModel;
  currentUser: string;
  messageModel: any = {};
  issueMessages: IssueMessageModel[] = [];

  constructor(
    private authService: AuthService,
    private issueMessageService: IssueMessageService,
    private errorService: ErrorService,
    private issueService: IssueService
  ) {}

  ngOnInit() {
    this.currentUser = this.authService.decodedToken.nameid;

    setTimeout(() => {
      this.getIssueMessages();
    }, 100);
  }

  sendMessage(form: any) {
    this.messageModel.issueId = this.issue.id;
    this.messageModel.senderId = this.currentUser;

    this.issueMessageService.addNewMessage(this.messageModel).subscribe(
      (data: IssueMessageModel) => {
        this.issueMessages.push(data);

        if (this.currentUser != this.issue.declarantId) {
          this.issueService
            .changeIssueStatus(this.issue.id, Status.Pending)
            .subscribe(
              () => {},
              (error) => {
                this.errorService.newError(error);
              }
            );
        } else {
          this.issueService
            .changeIssueStatus(this.issue.id, Status.Progress)
            .subscribe(
              () => {},
              (error) => {
                this.errorService.newError(error);
              }
            );
        }

        form.reset();
      },
      (error) => this.errorService.newError(error)
    );
  }

  getIssueMessages() {
    this.issueMessageService.getIssueMessages(this.issue.id).subscribe(
      (data) => {
        this.issueMessages = data;
      },
      (error) => {
        this.errorService.newError(error);
      }
    );
  }
}
