import { Routes } from "@angular/router";
import { AuthGuard } from "./core/auth.guard";
import { LoginComponent } from "./components/login/login.component";

export const AppRoutes: Routes = [
  { path: "", component: LoginComponent },
  {
    path: "",
    canActivate: [AuthGuard],
    runGuardsAndResolvers: "always",
    children: [
      {
        path: "admin",
        loadChildren: "./components/panels/panel.module#PanelModule",
      },
      {
        path: "user",
        loadChildren: "./components/panels/panel.module#PanelModule",
      },
    ],
  },
  { path: "**", redirectTo: "", pathMatch: "full" },
];
