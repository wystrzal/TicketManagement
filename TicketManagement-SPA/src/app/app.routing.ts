import { Routes } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { AuthGuard } from "./guards/auth.guard";

export const AppRoutes: Routes = [
  { path: "", component: LoginComponent },
  {
    path: "",
    canActivate: [AuthGuard],
    runGuardsAndResolvers: "always",
    children: [
      {
        path: "admin",
        loadChildren: "./admin-panel/admin-panel.module#AdminPanelModule",
      },
      {
        path: "home",
        loadChildren: "./user-panel/user-panel.module#UserPanelModule",
      },
    ],
  },
  { path: "**", redirectTo: "", pathMatch: "full" },
];
