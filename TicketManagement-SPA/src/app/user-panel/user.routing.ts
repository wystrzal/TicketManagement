import { Routes, RouterModule } from "@angular/router";
import { UserPanelComponent } from "./user-panel.component";
import { AuthGuard } from "../core/auth.guard";
import { UserIssuesResolver } from "../issues/resolvers/user-issues.resolver";
import { SearchFor } from "../models/enums/searchFor.enum";
import { IssuesComponent } from "../issues/issues.component";
import { NewIssueComponent } from "./new-issue/new-issue.component";

const routes: Routes = [
  {
    path: "user",
    component: UserPanelComponent,
    canActivate: [AuthGuard],
    runGuardsAndResolvers: "always",
    children: [
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
