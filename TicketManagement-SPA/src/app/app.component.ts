import { Component, OnInit } from "@angular/core";
import { JwtHelperService } from "@auth0/angular-jwt";
import { WrapperService } from "./shared/wrapper.service";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  styleUrls: ["./app.component.scss"],
})
export class AppComponent implements OnInit {
  title = "TicketManagement-SPA";

  jwtHelper = new JwtHelperService();

  constructor(private wrapperService: WrapperService) {}

  ngOnInit() {
    const token = localStorage.getItem("token");
    if (token) {
      this.wrapperService.AuthService.decodedToken = this.jwtHelper.decodeToken(
        token
      );
    }
  }
}
