import {
    HttpInterceptor,
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpErrorResponse,
  } from '@angular/common/http';
import { ThrowStmt } from '@angular/compiler';
  import { Injectable } from '@angular/core';
  import { Router } from '@angular/router';
  import { Observable } from 'rxjs'
    import { catchError} from 'rxjs/operators';
import { AuthService } from './auth-service.service';
  
  @Injectable()
  export class AuthInterceptor implements HttpInterceptor {
    public constructor(
      private readonly authService: AuthService,
      private readonly router: Router
    ) {}
  
    public intercept(
      request: HttpRequest<Record<string, unknown>>,
      next: HttpHandler
    ): Observable<HttpEvent<unknown>> {
      const token = this.authService.getToken();
      return next
        .handle(
          token
            ? request.clone({
                headers: request.headers.append(
                  'Authorization',
                  `Bearer ${token}`
                ),
              })
            : request
        )
        .pipe(
          catchError((error: HttpEvent<unknown>) => {
            if (error instanceof HttpErrorResponse) {
              if (error.status == 401) {
                console.error('Authentication error');
                this.authService.clearToken();
                this.router.navigate(['/login']);
              }
            }
            return new Observable<any>()
          })
        );
    }
  }