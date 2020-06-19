import {
  Directive,
  Input,
  ViewContainerRef,
  TemplateRef,
  OnInit,
} from "@angular/core";
import { WrapperService } from "../wrapper.service";

@Directive({
  selector: "[hasRole]",
})
export class HasRoleDirective implements OnInit {
  @Input() hasRole: string;
  isVisible = false;

  constructor(
    private viewContainerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>,
    private wrapperService: WrapperService
  ) {}

  ngOnInit() {
    const userRole = this.wrapperService.AuthService.decodedToken.role.toString();
    if (!userRole) {
      this.viewContainerRef.clear();
    }

    if (this.wrapperService.AuthService.roleMatch(this.hasRole)) {
      if (!this.isVisible) {
        this.isVisible = true;
        this.viewContainerRef.createEmbeddedView(this.templateRef);
      } else {
        this.isVisible = false;
        this.viewContainerRef.clear();
      }
    }
  }
}
