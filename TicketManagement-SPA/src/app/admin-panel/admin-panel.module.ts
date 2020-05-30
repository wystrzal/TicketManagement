import { NgModule } from "@angular/core";
import { AdminPanelComponent } from "./admin-panel.component";
import { SharedModule } from "../shared/shared.module";
import { AdminRoutes } from "./admin.routing";
import { TicketPanelComponent } from "./ticket-panel/ticket-panel.component";

@NgModule({
  declarations: [AdminPanelComponent, TicketPanelComponent],
  imports: [SharedModule, AdminRoutes],
})
export class AdminPanelModule {}
