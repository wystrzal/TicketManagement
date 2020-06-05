import { Component, OnInit, Output, EventEmitter } from "@angular/core";

@Component({
  selector: "app-issues-search",
  templateUrl: "./issues-search.component.html",
  styleUrls: ["./issues-search.component.scss"],
})
export class IssuesSearchComponent implements OnInit {
  @Output() startSearch = new EventEmitter();
  searchModel: any = { status: null };

  constructor() {}

  ngOnInit() {}

  search() {
    this.startSearch.emit(this.searchModel);
  }
}
