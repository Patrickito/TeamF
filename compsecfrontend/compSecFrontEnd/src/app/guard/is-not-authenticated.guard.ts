import { Injectable } from '@angular/core';
import {
  CanActivate,
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthService } from '../authProxy/auth-service.service';

@Injectable()
export class IsNotAuthenticatedGuard implements CanActivate {
  constructor(
    private readonly authService: AuthService,
    private readonly router: Router
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const isAuthenticated = !!this.authService.getToken();
    if (isAuthenticated) {
      this.router.navigate(['/user']);
    }
    return !isAuthenticated;
  }
}
