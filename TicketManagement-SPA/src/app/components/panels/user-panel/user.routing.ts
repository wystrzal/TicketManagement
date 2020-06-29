import { Routes, RouterModule } from "@angular/router";
import { UserPanelComponent } from "./user-panel.component";
import { AuthGuard } from "src/app/core/auth.guard";
import { IssueDetailComponent } from "../../issues/issue-detail/issue-detail.component";
import { IssuesComponent } from "../../issues/issues.component";
import { UserIssuesResolver } from "../../issues/resolvers/user-issues.resolver";
import { SearchFor } from "src/app/models/enums/searchFor.enum";
import { NewIssueComponent } from "../../issues/new-issue/new-issue.component";

const routes: Routes = [
  {
    path: "user",
    component: UserPanelComponent,
    canActivate: [AuthGuard],
    runGuardsAndResolvers: "always",
    children: [
      { path: "issue/:id", component: IssueDetailComponent },
      {
        path: "issues",
        component: IssuesComponent,
        resolve: { issues: UserIssuesResolver },
        data: { searchFor: SearchFor.UserIssues },
      },
      { path: "new-issue", component: NewIssueComponent },
      { path: "**", redirectTo: "issues", pathMatch: "full" },
    ],
  },
];

export const UserRoutes = RouterModule.forChild(routes);
