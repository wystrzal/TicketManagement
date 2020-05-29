import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NavComponent } from "src/app/shared/nav/nav.component";
import { ModalModule } from "ngx-bootstrap/modal";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { BrowserModule } from "@angular/platform-browser";

@NgModule({
  declarations: [NavComponent],
  imports: [
    CommonModule,
    ModalModule.forRoot(),
    FormsModule,
    RouterModule,
    BrowserModule,
  ],
  exports: [
    ModalModule,
    CommonModule,
    NavComponent,
    FormsModule,
    RouterModule,
    BrowserModule,
  ],
})
export class SharedModule {}
