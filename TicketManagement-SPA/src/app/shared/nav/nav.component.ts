import { Component, OnInit } from "@angular/core";
import { WrapperService } from "../wrapper.service";

@Component({
  selector: "app-nav",
  templateUrl: "./nav.component.html",
  styleUrls: ["./nav.component.scss"],
})
export class NavComponent implements OnInit {
  menuDisplayed = false;

  constructor(private wrapperService: WrapperService) {}

  ngOnInit() {}

  showMenu() {
    this.menuDisplayed = !this.menuDisplayed;
  }

  logout() {
    this.wrapperService.AuthService.logout();
  }
}
