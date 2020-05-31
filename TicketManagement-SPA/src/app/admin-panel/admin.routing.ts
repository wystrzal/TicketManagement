import { Routes, RouterModule } from "@angular/router";
import { AdminPanelModule } from "./admin-panel.module";
import { AuthGuard } from "../guards/auth.guard";
import { AdminPanelComponent } from "./admin-panel.component";
import { TicketPanelComponent } from "../shared/ticket-panel/ticket-panel.component";

const routes: Routes = [
  {
    path: "admin",
    runGuardsAndResolvers: "always",
    canActivate: [AuthGuard],
    component: AdminPanelComponent,
    data: { role: "admin" },
    children: [
      {
        path: "ticket",
        component: TicketPanelComponent,
      },
    ],
  },
];

export const AdminRoutes = RouterModule.forChild(routes);
