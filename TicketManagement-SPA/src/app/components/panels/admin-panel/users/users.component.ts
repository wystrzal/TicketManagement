import { Component, OnInit } from "@angular/core";
import { UserModel } from "src/app/models/user.model";
import { WrapperService } from "src/app/shared/wrapper.service";

@Component({
  selector: "app-users",
  templateUrl: "./users.component.html",
  styleUrls: ["./users.component.scss"],
})
export class UsersComponent implements OnInit {
  users: UserModel[];

  constructor(private wrapperService: WrapperService) {}

  ngOnInit() {
    this.getUsers();
    console.log(this.users);
  }

  getUsers() {
    this.wrapperService.AuthService.getUsers("all").subscribe(
      (data) => {
        this.users = data;
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }

  deleteUser(userId: string, userIndex: number) {
    this.wrapperService.AuthService.deleteUser(userId).subscribe(
      () => {
        this.users.splice(userIndex, 1);
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }
}
