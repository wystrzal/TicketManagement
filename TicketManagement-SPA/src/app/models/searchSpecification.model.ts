import { Status } from "./enums/status.enum";
import { SearchFor } from "./enums/searchFor.enum";

export interface SearchSpecificationModel {
  departament: string;
  status?: Status;
  title: string;
  declarantLastName: string;
  userId: string;
  pageIndex: number;
  pageSize: number;
  searchFor: SearchFor;
}
