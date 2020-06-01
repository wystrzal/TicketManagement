import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NavComponent } from "src/app/shared/nav/nav.component";
import { ModalModule } from "ngx-bootstrap/modal";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { TicketDetailComponent } from "./ticket-panel/ticket-detail/ticket-detail.component";
import { TicketPanelComponent } from "./ticket-panel/ticket-panel.component";
import { HasRoleDirective } from "../directives/hasRole.directive";
import { TicketsComponent } from "./tickets/tickets.component";
import { PaginationModule } from "ngx-bootstrap/pagination";

@NgModule({
  declarations: [
    NavComponent,
    TicketDetailComponent,
    TicketPanelComponent,
    HasRoleDirective,
    TicketsComponent,
  ],
  imports: [
    HttpClientModule,
    CommonModule,
    ModalModule.forRoot(),
    FormsModule,
    PaginationModule.forRoot(),
  ],
  exports: [NavComponent, FormsModule],
})
export class SharedModule {}
