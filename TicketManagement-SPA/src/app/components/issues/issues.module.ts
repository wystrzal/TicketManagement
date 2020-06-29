import { NgModule } from "@angular/core";
import { IssuesComponent } from "../issues/issues.component";
import { IssueDetailComponent } from "../issues/issue-detail/issue-detail.component";
import { IssueMessageComponent } from "../issues/issue-detail/issue-message/issue-message.component";
import { AllIssuesResolver } from "./resolvers/all-issues.resolver";
import { UserIssuesResolver } from "./resolvers/user-issues.resolver";
import { SupportIssuesResolver } from "./resolvers/support-issues.resolver";
import { IssuesSearchComponent } from "./issues-search/issues-search.component";
import { NewIssueComponent } from "./new-issue/new-issue.component";
import { SharedModule } from "src/app/shared/shared.module";

@NgModule({
  declarations: [
    IssuesComponent,
    IssueDetailComponent,
    IssueMessageComponent,
    IssuesSearchComponent,
    NewIssueComponent,
  ],
  imports: [SharedModule],
  providers: [AllIssuesResolver, UserIssuesResolver, SupportIssuesResolver],
})
export class IssuesModule {}
