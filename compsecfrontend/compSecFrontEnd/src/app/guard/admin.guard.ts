import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { AuthService } from '../authProxy/auth-service.service';
import jwt_decode from 'jwt-decode';

@Injectable()
export class AdminGuard implements CanActivate {
  constructor(
    private readonly authService: AuthService,
    private readonly router: Router
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const token = this.authService.getToken();
    if (token === null) {
      this.router.navigate(["/changePassword"])
      return false;
    }

    const decodedToken = jwt_decode<any>(token);
    if (!decodedToken) {
      this.router.navigate(["/changePassword"])
      return false;
    }
    const role = decodedToken.role;
    if(!!role.includes('Administrator')){
      return !!role.includes('Administrator')
    }
    else{
      this.router.navigate(["/changePassword"])
      return !!role.includes('Administrator')
    }
   
  }
}
