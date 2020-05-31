import { NgModule } from "@angular/core";

import { AppComponent } from "./app.component";
import { LoginComponent } from "./login/login.component";
import { SharedModule } from "./shared/shared.module";
import { CoreModule } from "./core/core.module";
import { HttpClientModule } from "@angular/common/http";
import { AppRoutes } from "./app.routing";
import { RouterModule } from "@angular/router";
import { AdminPanelModule } from "./admin-panel/admin-panel.module";
import { UserPanelModule } from "./user-panel/user-panel.module";

@NgModule({
  declarations: [AppComponent, LoginComponent],
  imports: [
    RouterModule.forRoot(AppRoutes),
    AdminPanelModule,
    UserPanelModule,
    SharedModule,
    CoreModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
