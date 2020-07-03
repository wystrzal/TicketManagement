import { Component, OnInit } from "@angular/core";
import { DepartamentModel } from "src/app/models/departament.model";
import { WrapperService } from "src/app/shared/wrapper.service";

@Component({
  selector: "app-new-user",
  templateUrl: "./new-user.component.html",
  styleUrls: ["./new-user.component.scss"],
})
export class NewUserComponent implements OnInit {
  departaments: DepartamentModel[];
  userModel: any = {};
  departamentModel: any = {};

  constructor(private wrapperService: WrapperService) {}

  ngOnInit() {
    this.getDepartaments();
  }

  deleteADepartament() {
    this.wrapperService.DepartamentService.deleteDepartament(
      this.departamentModel.id
    ).subscribe(
      () => {
        this.departaments.splice(
          this.departaments.indexOf(this.departamentModel.id)
        );
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }

  getDepartaments() {
    this.wrapperService.DepartamentService.getDepartaments().subscribe(
      (data) => {
        this.departaments = data;
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }

  createDepartament(form: any) {
    this.wrapperService.DepartamentService.addDepartament(
      this.departamentModel
    ).subscribe(
      () => {
        form.reset();
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }

  createUser(form: any) {
    this.userModel.departamentId = parseInt(this.userModel.departamentId);

    this.wrapperService.AuthService.createUser(this.userModel).subscribe(
      () => {
        form.reset();
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }
}
