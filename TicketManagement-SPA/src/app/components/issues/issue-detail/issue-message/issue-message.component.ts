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
    if (this.issue !== undefined) {
      this.getIssueMessages();
    }
  }

  ngOnInit() {
    this.currentUser = this.wrapperService.AuthService.decodedToken.nameid;
  }

  sendMessage(form: any, isSupportMessage: boolean) {
    this.SetMessageModel(isSupportMessage);

    this.wrapperService.IssueMessageService.addNewMessage(
      this.messageModel
    ).subscribe(
      (data: IssueMessageModel) => {
        this.PushMessages(data);
        this.ChangeIssueStatus();
        form.reset();
      },
      (error) => this.wrapperService.ErrorService.newError(error)
    );
  }

  private ChangeIssueStatus() {
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

      return;
    }

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

  private SetMessageModel(isSupportMessage: boolean) {
    this.messageModel.issueId = this.issue.id;
    this.messageModel.senderId = this.currentUser;
    this.messageModel.isSupportMessage = isSupportMessage;
  }

  private PushMessages(data: IssueMessageModel) {
    if (this.supportMessages) {
      this.issueSupportMessages.push(data);
      return;
    }

    this.issueUserMessages.push(data);
  }

  getIssueMessages() {
    this.GetUserMessages();
    this.GetSupportMessages();
  }

  private GetSupportMessages() {
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

  private GetUserMessages() {
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
  }
}
