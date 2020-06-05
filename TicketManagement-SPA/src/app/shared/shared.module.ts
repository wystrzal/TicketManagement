import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NavComponent } from "src/app/shared/nav/nav.component";
import { ModalModule } from "ngx-bootstrap/modal";
import { FormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { HasRoleDirective } from "./directives/hasRole.directive";
import { PaginationModule } from "ngx-bootstrap/pagination";

@NgModule({
  declarations: [NavComponent, HasRoleDirective],
  imports: [
    HttpClientModule,
    CommonModule,
    ModalModule.forRoot(),
    FormsModule,
    PaginationModule.forRoot(),
  ],
  exports: [NavComponent, FormsModule, PaginationModule, CommonModule],
})
export class SharedModule {}
