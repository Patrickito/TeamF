/* tslint:disable */
/* eslint-disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { BaseService } from '../base-service';
import { ApiConfiguration } from '../api-configuration';
import { StrictHttpResponse } from '../strict-http-response';
import { RequestBuilder } from '../request-builder';
import { Observable } from 'rxjs';
import { map, filter } from 'rxjs/operators';

import { AuthenticationDto } from '../models/authentication-dto';
import { PasswordChangeDto } from '../models/password-change-dto';
import { RoleChangeDto } from '../models/role-change-dto';
import { TokenDto } from '../models/token-dto';
import { UserDto } from '../models/user-dto';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation register
   */
  static readonly RegisterPath = '/Authentication/register';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `register()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  register$Response(params?: {
    body?: AuthenticationDto
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, AuthenticationService.RegisterPath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `register$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  register(params?: {
    body?: AuthenticationDto
  }): Observable<void> {

    return this.register$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation login
   */
  static readonly LoginPath = '/Authentication/login';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `login()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  login$Response(params?: {
    body?: AuthenticationDto
  }): Observable<StrictHttpResponse<TokenDto>> {

    const rb = new RequestBuilder(this.rootUrl, AuthenticationService.LoginPath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<TokenDto>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `login$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  login(params?: {
    body?: AuthenticationDto
  }): Observable<TokenDto> {

    return this.login$Response(params).pipe(
      map((r: StrictHttpResponse<TokenDto>) => r.body as TokenDto)
    );
  }

  /**
   * Path part for operation changePassword
   */
  static readonly ChangePasswordPath = '/Authentication/changePassword';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `changePassword()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  changePassword$Response(params?: {
    body?: PasswordChangeDto
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, AuthenticationService.ChangePasswordPath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `changePassword$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  changePassword(params?: {
    body?: PasswordChangeDto
  }): Observable<void> {

    return this.changePassword$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation changeRoles
   */
  static readonly ChangeRolesPath = '/Authentication/changeRoles';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `changeRoles()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  changeRoles$Response(params?: {
    body?: RoleChangeDto
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, AuthenticationService.ChangeRolesPath, 'post');
    if (params) {
      rb.body(params.body, 'application/json');
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `changeRoles$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  changeRoles(params?: {
    body?: RoleChangeDto
  }): Observable<void> {

    return this.changeRoles$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation getAllUsers
   */
  static readonly GetAllUsersPath = '/Authentication/users';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getAllUsers()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllUsers$Response(params?: {
  }): Observable<StrictHttpResponse<Array<UserDto>>> {

    const rb = new RequestBuilder(this.rootUrl, AuthenticationService.GetAllUsersPath, 'post');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<UserDto>>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `getAllUsers$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllUsers(params?: {
  }): Observable<Array<UserDto>> {

    return this.getAllUsers$Response(params).pipe(
      map((r: StrictHttpResponse<Array<UserDto>>) => r.body as Array<UserDto>)
    );
  }

  /**
   * Path part for operation getAllRoles
   */
  static readonly GetAllRolesPath = '/Authentication/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getAllRoles()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllRoles$Response(params: {
    id: string;
  }): Observable<StrictHttpResponse<Array<string>>> {

    const rb = new RequestBuilder(this.rootUrl, AuthenticationService.GetAllRolesPath, 'get');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<string>>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `getAllRoles$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllRoles(params: {
    id: string;
  }): Observable<Array<string>> {

    return this.getAllRoles$Response(params).pipe(
      map((r: StrictHttpResponse<Array<string>>) => r.body as Array<string>)
    );
  }

  /**
   * Path part for operation deleteUser
   */
  static readonly DeleteUserPath = '/Authentication/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `deleteUser()` instead.
   *
   * This method doesn't expect any request body.
   */
  deleteUser$Response(params: {
    id: string;
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, AuthenticationService.DeleteUserPath, 'delete');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `deleteUser$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  deleteUser(params: {
    id: string;
  }): Observable<void> {

    return this.deleteUser$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

}
