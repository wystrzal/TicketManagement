import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { DepartamentModel } from "src/app/models/departament.model";

@Injectable({
  providedIn: "root",
})
export class DepartamentService {
  baseUrl = environment.apiUrl + "departament/";

  constructor(private http: HttpClient) {}

  getDepartaments(): Observable<DepartamentModel[]> {
    return this.http.get<DepartamentModel[]>(this.baseUrl);
  }

  addDepartament(departamentModel: any) {
    return this.http.post(this.baseUrl, departamentModel);
  }
}
