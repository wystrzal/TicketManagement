import { Component, OnInit } from "@angular/core";
import { AuthService } from "../core/auth.service";
import { ErrorService } from "../core/helpers/error.service";
import { Router } from "@angular/router";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit {
  loginModel: any = {};

  constructor(
    private authService: AuthService,
    private errorService: ErrorService,
    private router: Router
  ) {}

  ngOnInit() {}

  login() {
    this.authService.login(this.loginModel).subscribe(
      () => {
        if (this.authService.decodedToken.role == "admin") {
          this.router.navigate(["admin/ticket"]);
        } else {
          this.router.navigate(["home"]);
        }
      },
      (error) => {
        this.errorService.newError(error);
      }
    );
  }
}
