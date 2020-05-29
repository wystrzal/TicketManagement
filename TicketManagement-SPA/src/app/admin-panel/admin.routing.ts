import { Routes, RouterModule } from "@angular/router";
import { AdminPanelModule } from "./admin-panel.module";
import { AuthGuard } from "../guards/auth.guard";
import { AdminPanelComponent } from "./admin-panel.component";

const routes: Routes = [
  {
    path: "home/admin",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    component: AdminPanelComponent,
  },
];

export const AdminRoutes = RouterModule.forChild(routes);
