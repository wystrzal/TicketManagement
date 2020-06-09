import { NgModule } from "@angular/core";
import { UserPanelComponent } from "./user-panel.component";
import { SharedModule } from "../shared/shared.module";
import { UserRoutes } from "./user.routing";

@NgModule({
  declarations: [UserPanelComponent],
  imports: [SharedModule, UserRoutes],
})
export class UserPanelModule {}
