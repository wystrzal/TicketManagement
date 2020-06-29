import { Routes, RouterModule } from "@angular/router";
import { NewUserComponent } from "./new-user/new-user.component";
import { UsersComponent } from "./users/users.component";
import { AuthGuard } from "src/app/core/auth.guard";
import { AdminPanelComponent } from "./admin-panel.component";
import { IssuesComponent } from "../../issues/issues.component";
import { IssueDetailComponent } from "../../issues/issue-detail/issue-detail.component";
import { AllIssuesResolver } from "../../issues/resolvers/all-issues.resolver";
import { SearchFor } from "src/app/models/enums/searchFor.enum";
import { SupportIssuesResolver } from "../../issues/resolvers/support-issues.resolver";

const routes: Routes = [
  {
    path: "admin",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    component: AdminPanelComponent,
    data: { role: "admin" },
    children: [
      { path: "issue/:id", component: IssueDetailComponent },
      {
        path: "issues",
        component: IssuesComponent,
        resolve: { issues: AllIssuesResolver },
        data: { searchFor: SearchFor.AllIssues },
      },
      { path: "add-user", component: NewUserComponent },
      { path: "users", component: UsersComponent },
      {
        path: "assigned-issues",
        component: IssuesComponent,
        resolve: { issues: SupportIssuesResolver },
        data: { searchFor: SearchFor.SupportIssues },
      },
      { path: "**", redirectTo: "issues", pathMatch: "full" },
    ],
  },
];

export const AdminRoutes = RouterModule.forChild(routes);
