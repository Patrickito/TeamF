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

import { Comment } from '../models/comment';

@Injectable({
  providedIn: 'root',
})
export class CommentService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation getCaffFileComment
   */
  static readonly GetCaffFileCommentPath = '/api/Comment/CaffFile/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getCaffFileComment()` instead.
   *
   * This method doesn't expect any request body.
   */
  getCaffFileComment$Response(params: {
    id: number;
  }): Observable<StrictHttpResponse<Array<Comment>>> {

    const rb = new RequestBuilder(this.rootUrl, CommentService.GetCaffFileCommentPath, 'get');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<Comment>>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `getCaffFileComment$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getCaffFileComment(params: {
    id: number;
  }): Observable<Array<Comment>> {

    return this.getCaffFileComment$Response(params).pipe(
      map((r: StrictHttpResponse<Array<Comment>>) => r.body as Array<Comment>)
    );
  }

  /**
   * Path part for operation deleteCaffFileComment
   */
  static readonly DeleteCaffFileCommentPath = '/api/Comment/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `deleteCaffFileComment()` instead.
   *
   * This method doesn't expect any request body.
   */
  deleteCaffFileComment$Response(params: {
    id: number;
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, CommentService.DeleteCaffFileCommentPath, 'delete');
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
   * To access the full response (for headers, for example), `deleteCaffFileComment$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  deleteCaffFileComment(params: {
    id: number;
  }): Observable<void> {

    return this.deleteCaffFileComment$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation addCaffFileComment
   */
  static readonly AddCaffFileCommentPath = '/api/Comment';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `addCaffFileComment()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  addCaffFileComment$Response(params?: {
    body?: Comment
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, CommentService.AddCaffFileCommentPath, 'post');
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
   * To access the full response (for headers, for example), `addCaffFileComment$Response()` instead.
   *
   * This method sends `application/json` and handles request body of type `application/json`.
   */
  addCaffFileComment(params?: {
    body?: Comment
  }): Observable<void> {

    return this.addCaffFileComment$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

}
