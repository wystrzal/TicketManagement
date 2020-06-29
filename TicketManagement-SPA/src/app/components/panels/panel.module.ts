import { NgModule } from "@angular/core";
import { UserPanelComponent } from "./user-panel/user-panel.component";
import { SharedModule } from "src/app/shared/shared.module";
import { UserRoutes } from "./user-panel/user.routing";
import { AdminPanelComponent } from "./admin-panel/admin-panel.component";
import { UsersComponent } from "./admin-panel/users/users.component";
import { AdminRoutes } from "./admin-panel/admin.routing";
import { NewUserComponent } from "./admin-panel/new-user/new-user.component";

@NgModule({
  declarations: [
    UserPanelComponent,
    AdminPanelComponent,
    UsersComponent,
    UsersComponent,
    NewUserComponent,
  ],
  imports: [SharedModule, UserRoutes, AdminRoutes],
})
export class PanelModule {}
