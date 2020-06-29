import { Component, OnInit, Input, OnChanges } from "@angular/core";
import { IssueModel } from "src/app/models/issue.model";
import { IssueMessageModel } from "src/app/models/issueMessage.model";
import { Status } from "src/app/models/enums/status.enum";
import { WrapperService } from "src/app/shared/wrapper.service";

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

  constructor(private wrapperService: WrapperService) {}

  ngOnChanges() {
    setTimeout(() => {
      this.getIssueMessages();
    }, 500);
  }

  ngOnInit() {
    this.currentUser = this.wrapperService.AuthService.decodedToken.nameid;
  }

  sendMessage(form: any, isSupportMessage: boolean) {
    this.messageModel.issueId = this.issue.id;
    this.messageModel.senderId = this.currentUser;
    this.messageModel.isSupportMessage = isSupportMessage;

    this.wrapperService.IssueMessageService.addNewMessage(
      this.messageModel
    ).subscribe(
      (data: IssueMessageModel) => {
        if (this.supportMessages) {
          this.issueSupportMessages.push(data);
        } else {
          this.issueUserMessages.push(data);
        }

        if (this.currentUser != this.issue.declarantId) {
          this.wrapperService.IssueService.changeIssueStatus(
            this.issue.id,
            Status.Pending
          ).subscribe(
            () => {},
            (error) => {
              this.wrapperService.ErrorService.newError(error);
            }
          );
        } else {
          this.wrapperService.IssueService.changeIssueStatus(
            this.issue.id,
            Status.Progress
          ).subscribe(
            () => {},
            (error) => {
              this.wrapperService.ErrorService.newError(error);
            }
          );
        }

        form.reset();
      },
      (error) => this.wrapperService.ErrorService.newError(error)
    );
  }

  getIssueMessages() {
    this.wrapperService.IssueMessageService.getIssueMessages(
      this.issue.id,
      false
    ).subscribe(
      (data) => {
        this.issueUserMessages = data;
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );

    this.wrapperService.IssueMessageService.getIssueMessages(
      this.issue.id,
      true
    ).subscribe(
      (data) => {
        this.issueSupportMessages = data;
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }
}
