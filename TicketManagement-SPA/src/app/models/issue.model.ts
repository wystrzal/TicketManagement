import { Status } from "./enums/status.enum";

export interface IssueModel {
  id: number;
  title: string;
  status: Status;
  declarant: string;
  departament: string;
}
