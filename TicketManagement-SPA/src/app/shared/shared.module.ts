import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NavComponent } from "src/app/shared/nav/nav.component";
import { ModalModule } from "ngx-bootstrap/modal";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { BrowserModule } from "@angular/platform-browser";
import { HttpClientModule } from "@angular/common/http";

@NgModule({
  declarations: [NavComponent],
  imports: [HttpClientModule, CommonModule, ModalModule.forRoot(), FormsModule],
  exports: [
    HttpClientModule,
    ModalModule,
    CommonModule,
    NavComponent,
    FormsModule,
  ],
})
export class SharedModule {}
