import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/core/auth.service";
import { UserModel } from "src/app/models/user.model";
import { ErrorService } from "src/app/core/helpers/error.service";

@Component({
  selector: "app-users",
  templateUrl: "./users.component.html",
  styleUrls: ["./users.component.scss"],
})
export class UsersComponent implements OnInit {
  users: UserModel[];

  constructor(
    private authService: AuthService,
    private errorService: ErrorService
  ) {}

  ngOnInit() {
    this.getUsers();
    console.log(this.users);
  }

  getUsers() {
    this.authService.getUsers("all").subscribe(
      (data) => {
        this.users = data;
      },
      (error) => {
        this.errorService.newError(error);
      }
    );
  }

  deleteUser(userId: string, userIndex: number) {
    this.authService.deleteUser(userId).subscribe(
      () => {
        this.users.splice(userIndex, 1);
      },
      (error) => {
        this.errorService.newError(error);
      }
    );
  }
}
