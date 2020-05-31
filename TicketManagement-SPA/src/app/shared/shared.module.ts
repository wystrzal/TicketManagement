import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NavComponent } from "src/app/shared/nav/nav.component";
import { ModalModule } from "ngx-bootstrap/modal";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { TicketDetailComponent } from "./ticket-panel/ticket-detail/ticket-detail.component";
import { TicketPanelComponent } from "./ticket-panel/ticket-panel.component";
import { HasRoleDirective } from "../directives/hasRole.directive";

@NgModule({
  declarations: [
    NavComponent,
    TicketDetailComponent,
    TicketPanelComponent,
    HasRoleDirective,
  ],
  imports: [HttpClientModule, CommonModule, ModalModule.forRoot(), FormsModule],
  exports: [NavComponent, FormsModule],
})
export class SharedModule {}
