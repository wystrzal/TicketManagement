import { NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { SharedModule } from "./shared/shared.module";
import { CoreModule } from "./core/core.module";
import { AppRoutes } from "./app.routing";
import { RouterModule } from "@angular/router";
import { IssuesModule } from "./components/issues/issues.module";
import { PanelModule } from "./components/panels/panel.module";
import { LoginComponent } from "./components/login/login.component";

@NgModule({
  declarations: [AppComponent, LoginComponent],
  imports: [
    RouterModule.forRoot(AppRoutes),
    IssuesModule,
    PanelModule,
    SharedModule,
    CoreModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
