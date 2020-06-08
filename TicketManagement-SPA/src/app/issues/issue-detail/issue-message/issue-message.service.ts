import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { IssueMessageModel } from "src/app/models/issueMessage.model";

@Injectable({
  providedIn: "root",
})
export class IssueMessageService {
  baseUrl = environment.apiUrl + "message/";

  constructor(private http: HttpClient) {}

  addNewMessage(messageModel: any) {
    return this.http.post(this.baseUrl, messageModel);
  }

  getIssueMessages(issueId: number): Observable<IssueMessageModel[]> {
    return this.http.get<IssueMessageModel[]>(this.baseUrl + issueId);
  }
}
