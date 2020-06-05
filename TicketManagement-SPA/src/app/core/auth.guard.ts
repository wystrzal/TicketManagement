import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot } from "@angular/router";
import { ErrorService } from "./helpers/error.service";
import { AuthService } from "./auth.service";

@Injectable({
  providedIn: "root",
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService,
    private router: Router,
    private errorService: ErrorService
  ) {}

  canActivate(next: ActivatedRouteSnapshot): boolean {
    const role = next.data.role as string;
    if (role) {
      const match = this.authService.roleMatch(role);
      if (match) {
        return true;
      } else {
        this.router.navigate([""]);
        this.errorService.newError("Unauthorized.");
      }
    }

    if (this.authService.loggedIn()) {
      return true;
    }

    this.errorService.newError("Unauthorized.");
    this.router.navigate([""]);
    return false;
  }
}
