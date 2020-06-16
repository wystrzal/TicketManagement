export interface IssueModel {
  id: number;
  title: string;
  status: string;
  priority: string;
  declarant: string;
  declarantId: string;
  departament: string;
  description: string;
  assignedSupport: string[];
}
