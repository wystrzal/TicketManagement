import { NgModule } from "@angular/core";
import { UserPanelComponent } from "./user-panel.component";
import { SharedModule } from "../shared/shared.module";
import { UserRoutes } from "./user.routing";
import { NewIssueComponent } from "./new-issue/new-issue.component";

@NgModule({
  declarations: [UserPanelComponent, NewIssueComponent],
  imports: [SharedModule, UserRoutes],
})
export class UserPanelModule {}
