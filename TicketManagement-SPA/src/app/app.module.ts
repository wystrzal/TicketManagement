import { NgModule } from "@angular/core";

import { AppComponent } from "./app.component";
import { LoginComponent } from "./login/login.component";
import { SharedModule } from "./shared/shared.module";
import { CoreModule } from "./core/core.module";
import { HttpClientModule } from "@angular/common/http";
import { AppRoutes } from "./app.routing";
import { RouterModule } from "@angular/router";

@NgModule({
  declarations: [AppComponent, LoginComponent],
  imports: [
    SharedModule,
    CoreModule,
    HttpClientModule,
    RouterModule.forRoot(AppRoutes),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
