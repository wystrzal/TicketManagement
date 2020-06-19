import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { WrapperService } from "../shared/wrapper.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit {
  loginModel: any = {};

  constructor(private wrapperService: WrapperService, private router: Router) {}

  ngOnInit() {}

  login() {
    this.wrapperService.AuthService.login(this.loginModel).subscribe(
      () => {
        if (
          this.wrapperService.AuthService.decodedToken.role.indexOf("admin") !==
          -1
        ) {
          this.router.navigate(["admin/issues"]);
        } else {
          this.router.navigate(["user/issues"]);
        }
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }
}
