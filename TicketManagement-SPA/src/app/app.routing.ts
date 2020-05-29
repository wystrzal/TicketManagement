import { Routes } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { AuthGuard } from "./guards/auth.guard";

export const AppRoutes: Routes = [{ path: "", component: LoginComponent }];
