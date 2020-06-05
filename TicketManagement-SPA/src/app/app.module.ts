import { NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { LoginComponent } from "./login/login.component";
import { SharedModule } from "./shared/shared.module";
import { CoreModule } from "./core/core.module";
import { AppRoutes } from "./app.routing";
import { RouterModule } from "@angular/router";
import { AdminPanelModule } from "./admin-panel/admin-panel.module";
import { UserPanelModule } from "./user-panel/user-panel.module";
import { IssuesModule } from "./issues/issues.module";
import { IssuesComponent } from "./issues/issues.component";

@NgModule({
  declarations: [AppComponent, LoginComponent],
  imports: [
    RouterModule.forRoot(AppRoutes),
    AdminPanelModule,
    IssuesModule,
    UserPanelModule,
    SharedModule,
    CoreModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
