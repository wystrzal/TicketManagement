import {
  Component,
  OnInit,
  Output,
  EventEmitter,
  Input,
  OnChanges,
} from "@angular/core";
import { DepartamentModel } from "src/app/models/departament.model";
import { WrapperService } from "src/app/shared/wrapper.service";

@Component({
  selector: "app-issues-search",
  templateUrl: "./issues-search.component.html",
  styleUrls: ["./issues-search.component.scss"],
})
export class IssuesSearchComponent implements OnInit, OnChanges {
  @Output() startSearch = new EventEmitter();
  @Input() issueCount: any;
  searchModel: any = { status: null, departament: null, priority: null };
  searchStatusList: any[];
  searchPriorityList: any[];
  departaments: DepartamentModel[];

  constructor(private wrapperService: WrapperService) {}

  ngOnChanges() {
    this.searchStatusList = [
      { status: "New", value: 1, count: this.issueCount.newIssue },
      { status: "Open", value: 2, count: this.issueCount.openIssue },
      { status: "Progress", value: 3, count: this.issueCount.progressIssue },
      { status: "Pending", value: 4, count: this.issueCount.pendingIssue },
      { status: "Close", value: 5, count: "-" },
    ];
    this.searchPriorityList = [
      { priority: "Low", value: 1 },
      { priority: "Medium", value: 2 },
      { priority: "High", value: 3 },
      { priority: "Very High", value: 4 },
    ];
  }

  ngOnInit() {
    this.getDepartaments();
  }

  getDepartaments() {
    this.wrapperService.IssueService.getIssueDepartaments().subscribe(
      (data) => {
        this.departaments = data;
      },
      (error) => {
        this.wrapperService.ErrorService.newError(error);
      }
    );
  }

  search() {
    this.searchModel.pageIndex = 1;
    this.startSearch.emit(this.searchModel);
    window.scroll(0, 0);
  }

  reset() {
    this.searchModel = { status: null, departament: null, priority: null };
    this.search();
  }
}
