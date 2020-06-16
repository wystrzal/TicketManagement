import { Component, OnInit, Input, OnChanges } from "@angular/core";
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
export class IssueMessageComponent implements OnInit, OnChanges {
  @Input() issue: IssueModel;
  currentUser: string;
  messageModel: any = {};
  issueUserMessages: IssueMessageModel[] = [];
  issueSupportMessages: IssueMessageModel[] = [];
  supportMessages = false;

  constructor(
    private authService: AuthService,
    private issueMessageService: IssueMessageService,
    private errorService: ErrorService,
    private issueService: IssueService
  ) {}

  ngOnChanges() {
    setTimeout(() => {
      this.getIssueMessages();
    }, 100);
  }

  ngOnInit() {
    this.currentUser = this.authService.decodedToken.nameid;
  }

  sendMessage(form: any, isSupportMessage: boolean) {
    this.messageModel.issueId = this.issue.id;
    this.messageModel.senderId = this.currentUser;
    this.messageModel.isSupportMessage = isSupportMessage;

    this.issueMessageService.addNewMessage(this.messageModel).subscribe(
      (data: IssueMessageModel) => {
        if (this.supportMessages) {
          this.issueSupportMessages.push(data);
        } else {
          this.issueUserMessages.push(data);
        }

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
    this.issueMessageService.getIssueMessages(this.issue.id, false).subscribe(
      (data) => {
        this.issueUserMessages = data;
      },
      (error) => {
        this.errorService.newError(error);
      }
    );

    this.issueMessageService.getIssueMessages(this.issue.id, true).subscribe(
      (data) => {
        this.issueSupportMessages = data;
      },
      (error) => {
        this.errorService.newError(error);
      }
    );
  }
}
