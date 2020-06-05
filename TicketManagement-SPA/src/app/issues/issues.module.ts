import { NgModule } from "@angular/core";
import { IssuesComponent } from "../issues/issues.component";
import { IssueDetailComponent } from "../issues/issue-detail/issue-detail.component";
import { IssueMessageComponent } from "../issues/issue-detail/issue-message/issue-message.component";
import { AllIssuesResolver } from "./resolvers/all-issues.resolver";
import { SharedModule } from "../shared/shared.module";
import { UserIssuesResolver } from "./resolvers/user-issues.resolver";
import { SupportIssuesResolver } from "./resolvers/support-issues.resolver";
import { IssuesSearchComponent } from "./issues-search/issues-search.component";

@NgModule({
  declarations: [
    IssuesComponent,
    IssueDetailComponent,
    IssueMessageComponent,
    IssuesSearchComponent,
  ],
  imports: [SharedModule],
  providers: [AllIssuesResolver, UserIssuesResolver, SupportIssuesResolver],
})
export class IssuesModule {}
