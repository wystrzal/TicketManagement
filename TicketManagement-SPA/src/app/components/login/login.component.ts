import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { retry } from "rxjs/operators";
import { WrapperService } from "src/app/shared/wrapper.service";

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
        this.NavigateToAppropiatePage();
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }

  private NavigateToAppropiatePage() {
    if (
      this.wrapperService.AuthService.decodedToken.role.indexOf("admin") !== -1
    ) {
      this.router.navigate(["admin/issues"]);
      return;
    }

    this.router.navigate(["user/issues"]);
  }
}
