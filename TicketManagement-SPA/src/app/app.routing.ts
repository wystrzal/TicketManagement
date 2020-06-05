import { Routes } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { AuthGuard } from "./core/auth.guard";

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
        data: { role: "admin" },
      },
      {
        path: "user",
        loadChildren: "./user-panel/user-panel.module#UserPanelModule",
        data: { role: "user" },
      },
    ],
  },
  { path: "**", redirectTo: "", pathMatch: "full" },
];
