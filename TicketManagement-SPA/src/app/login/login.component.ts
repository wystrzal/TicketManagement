import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from "../core/auth.service";
import { ErrorService } from "../core/helpers/error.service";

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
        if (this.authService.decodedToken.role.indexOf("admin") !== -1) {
          this.router.navigate(["admin/issues"]);
        } else {
          this.router.navigate(["user/issues"]);
        }
      },
      (error) => {
        this.errorService.newError(error);
      }
    );
  }
}
