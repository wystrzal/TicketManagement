import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/core/auth.service";

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.scss"],
})
export class NavComponent implements OnInit {
  menuDisplayed = false;

  constructor(private authService: AuthService) {}

  ngOnInit() {}

  showMenu() {
    this.menuDisplayed = !this.menuDisplayed;
  }

  logout() {
    this.authService.logout();
  }
}
