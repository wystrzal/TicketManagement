import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot } from "@angular/router";
import { WrapperService } from "../shared/wrapper.service";

@Injectable({
  providedIn: "root",
})
export class AuthGuard implements CanActivate {
  constructor(private wrapperService: WrapperService, private router: Router) {}

  canActivate(next: ActivatedRouteSnapshot): boolean {
    const role = next.data.role as string;
    if (role) {
      const match = this.wrapperService.AuthService.roleMatch(role);
      if (!match) {
        return this.Unauthorized();
      }    
    }
     
    if (this.wrapperService.AuthService.loggedIn()) {
      return true;
    }

    return this.Unauthorized();
  }

  private Unauthorized() {
    this.wrapperService.ErrorService.newError("Unauthorized.");
    this.router.navigate([""]);
    return false;
  }
}
