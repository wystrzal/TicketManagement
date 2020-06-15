import { Status } from "./enums/status.enum";
import { SearchFor } from "./enums/searchFor.enum";
import { Priority } from "./enums/priority.enum";

export interface SearchSpecificationModel {
  departament: string;
  status?: Status;
  priority?: Priority;
  title: string;
  declarantLastName: string;
  userId: string;
  pageIndex: number;
  pageSize: number;
  searchFor: SearchFor;
}
