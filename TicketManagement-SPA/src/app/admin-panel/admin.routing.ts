import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "../core/auth.guard";
import { AdminPanelComponent } from "./admin-panel.component";
import { IssuesComponent } from "../issues/issues.component";
import { SearchFor } from "../models/enums/searchFor.enum";
import { AllIssuesResolver } from "../issues/resolvers/all-issues.resolver";
import { SupportIssuesResolver } from "../issues/resolvers/support-issues.resolver";
import { IssueDetailComponent } from "../issues/issue-detail/issue-detail.component";

const routes: Routes = [
  {
    path: "admin",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    component: AdminPanelComponent,
    children: [
      { path: "issue/:id", component: IssueDetailComponent },
      {
        path: "issues",
        component: IssuesComponent,
        resolve: { issues: AllIssuesResolver },
        data: { searchFor: SearchFor.AllIssues },
      },
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
