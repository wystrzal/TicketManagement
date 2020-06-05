import {
  Directive,
  Input,
  ViewContainerRef,
  TemplateRef,
  OnInit,
} from "@angular/core";
import { AuthService } from "../../core/auth.service";

@Directive({
  selector: "[hasRole]",
})
export class HasRoleDirective implements OnInit {
  @Input() hasRole: string;
  isVisible = false;

  constructor(
    private viewContainerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>,
    private authService: AuthService
  ) {}

  ngOnInit() {
    const userRole = this.authService.decodedToken.role.toString();
    if (!userRole) {
      this.viewContainerRef.clear();
    }

    if (this.authService.roleMatch(this.hasRole)) {
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
