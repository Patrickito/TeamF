import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { AuthenticationDto, TokenDto } from '../api/models';
import { AuthenticationService } from '../api/services';

const TOKEN_KEY = 'AUTH_TOKENCSHW';

@Injectable()
export class AuthService {
  constructor(private readonly authService: AuthenticationService) {}

  public login(data: AuthenticationDto): Observable<TokenDto> {
    return this.authService
      .login({ body: data })
      .pipe(tap((token) => localStorage.setItem(TOKEN_KEY, token.token!!)));
  }

  public register(data: AuthenticationDto): Observable<void> {
    return this.authService.register({ body: data });
  }


  public getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  }

  public clearToken(): void {
    localStorage.removeItem(TOKEN_KEY);
  }
}
