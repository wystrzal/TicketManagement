import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
import { map } from "rxjs/operators";
import { JwtHelperService } from "@auth0/angular-jwt";
import { Router } from "@angular/router";
import { UserModel } from "../models/user.model";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  baseUrl = environment.apiUrl + "account/";
  jwtHelper = new JwtHelperService();
  decodedToken: any;

  constructor(private http: HttpClient, private route: Router) {}

  login(model: any) {
    return this.http.post(this.baseUrl + "login", model).pipe(
      map((response: any) => {
        if (response) {
          this.SetJWTToken(response);
        }
      })
    );
  }

  private SetJWTToken(response: any) {
    localStorage.setItem("token", response.token);
    this.decodedToken = this.jwtHelper.decodeToken(response.token);
  }

  logout() {
    localStorage.removeItem("token");
    this.decodedToken = null;
    this.route.navigate([""]);
  }

  loggedIn() {
    const token = localStorage.getItem("token");
    return !this.jwtHelper.isTokenExpired(token);
  }

  roleMatch(allowedRole: string): boolean {
    if (!this.decodedToken) {  
      return false;
    }

    const userRole = this.decodedToken.role.toString();

    if (userRole.indexOf(allowedRole) !== -1) {
      return true;
    }
  }

  createUser(userModel: any) {
    return this.http.post(this.baseUrl + "register", userModel);
  }

  getUsers(departament?: string): Observable<UserModel[]> {
    return this.http.get<UserModel[]>(this.baseUrl + departament);
  }

  deleteUser(userId: string) {
    return this.http.delete(this.baseUrl + userId, {});
  }
}
