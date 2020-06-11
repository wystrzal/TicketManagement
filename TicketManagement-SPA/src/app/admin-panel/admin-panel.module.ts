import { NgModule } from "@angular/core";
import { AdminPanelComponent } from "./admin-panel.component";
import { SharedModule } from "../shared/shared.module";
import { AdminRoutes } from "./admin.routing";
import { NewUserComponent } from "./new-user/new-user.component";

@NgModule({
  declarations: [AdminPanelComponent, NewUserComponent],
  imports: [SharedModule, AdminRoutes],
})
export class AdminPanelModule {}
