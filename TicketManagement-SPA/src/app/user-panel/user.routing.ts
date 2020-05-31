import { Routes, RouterModule } from "@angular/router";
import { UserPanelComponent } from "./user-panel.component";
import { AuthGuard } from "../guards/auth.guard";

const routes: Routes = [
  {
    path: "home",
    component: UserPanelComponent,
    canActivate: [AuthGuard],
    runGuardsAndResolvers: "always",
    data: { role: "user" },
  },
];

export const UserRoutes = RouterModule.forChild(routes);
