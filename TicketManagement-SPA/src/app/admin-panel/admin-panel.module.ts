import { NgModule } from "@angular/core";
import { AdminPanelComponent } from "./admin-panel.component";
import { SharedModule } from "../shared/shared.module";
import { AdminRoutes } from "./admin.routing";

@NgModule({
  declarations: [AdminPanelComponent],
  imports: [SharedModule, AdminRoutes],
})
export class AdminPanelModule {}
