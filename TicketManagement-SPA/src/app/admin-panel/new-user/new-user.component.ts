import { Component, OnInit } from "@angular/core";
import { DepartamentService } from "./departament.service";
import { ErrorService } from "src/app/core/helpers/error.service";
import { AuthService } from "src/app/core/auth.service";
import { DepartamentModel } from "src/app/models/departament.model";

@Component({
  selector: "app-new-user",
  templateUrl: "./new-user.component.html",
  styleUrls: ["./new-user.component.scss"],
})
export class NewUserComponent implements OnInit {
  departaments: DepartamentModel[];
  userModel: any = {};
  departamentModel: any = {};

  constructor(
    private departamentService: DepartamentService,
    private errorService: ErrorService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    this.getDepartaments();
  }

  getDepartaments() {
    this.departamentService.getDepartaments().subscribe(
      (data) => {
        this.departaments = data;
      },
      (error) => {
        this.errorService.newError(error);
      }
    );
  }

  createDepartament(form: any) {
    this.departamentService.addDepartament(this.departamentModel).subscribe(
      () => {
        form.reset();
      },
      (error) => {
        this.errorService.newError(error);
      }
    );
  }

  createUser(form: any) {
    this.userModel.departamentId = parseInt(this.userModel.departamentId);
    console.log(this.userModel);
    this.authService.createUser(this.userModel).subscribe(
      () => {
        form.reset();
      },
      (error) => {
        this.errorService.newError(error);
      }
    );
  }
}
