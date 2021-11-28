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

import { CaffEntity } from '../models/caff-entity';

@Injectable({
  providedIn: 'root',
})
export class CaffService extends BaseService {
  constructor(
    config: ApiConfiguration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * Path part for operation getAllCaffFiles
   */
  static readonly GetAllCaffFilesPath = '/api/Caff';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getAllCaffFiles()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllCaffFiles$Response(params?: {
  }): Observable<StrictHttpResponse<Array<CaffEntity>>> {

    const rb = new RequestBuilder(this.rootUrl, CaffService.GetAllCaffFilesPath, 'get');
    if (params) {
    }

    return this.http.request(rb.build({
      responseType: 'json',
      accept: 'application/json'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<any>) => {
        return r as StrictHttpResponse<Array<CaffEntity>>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `getAllCaffFiles$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getAllCaffFiles(params?: {
  }): Observable<Array<CaffEntity>> {

    return this.getAllCaffFiles$Response(params).pipe(
      map((r: StrictHttpResponse<Array<CaffEntity>>) => r.body as Array<CaffEntity>)
    );
  }

  /**
   * Path part for operation uploadCaffFIle
   */
  static readonly UploadCaffFIlePath = '/api/Caff';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `uploadCaffFIle()` instead.
   *
   * This method doesn't expect any request body.
   */
  uploadCaffFIle$Response(params?: {
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, CaffService.UploadCaffFIlePath, 'post');
    if (params) {
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
   * To access the full response (for headers, for example), `uploadCaffFIle$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  uploadCaffFIle(params?: {
  }): Observable<void> {

    return this.uploadCaffFIle$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation deleteCaffFile
   */
  static readonly DeleteCaffFilePath = '/api/Caff/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `deleteCaffFile()` instead.
   *
   * This method doesn't expect any request body.
   */
  deleteCaffFile$Response(params: {
    id: number;
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, CaffService.DeleteCaffFilePath, 'delete');
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
   * To access the full response (for headers, for example), `deleteCaffFile$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  deleteCaffFile(params: {
    id: number;
  }): Observable<void> {

    return this.deleteCaffFile$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

  /**
   * Path part for operation getCaffFile
   */
  static readonly GetCaffFilePath = '/api/Caff/CaffFile/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getCaffFile()` instead.
   *
   * This method doesn't expect any request body.
   */
  getCaffFile$Response(params: {
    id: number;
  }): Observable<StrictHttpResponse<CaffEntity>> {

    const rb = new RequestBuilder(this.rootUrl, CaffService.GetCaffFilePath, 'get');
    if (params) {
      rb.path('id', params.id, {});
    }

    return this.http.request(rb.build({
      responseType: 'text',
      accept: '*/*'
    })).pipe(
      filter((r: any) => r instanceof HttpResponse),
      map((r: HttpResponse<CaffEntity>) => {
        return (r as HttpResponse<CaffEntity>).clone() as StrictHttpResponse<CaffEntity>;
      })
    );
  }

  /**
   * This method provides access to only to the response body.
   * To access the full response (for headers, for example), `getCaffFile$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getCaffFile(params: {
    id: number;
  }): Observable<CaffEntity> {

    return this.getCaffFile$Response(params).pipe(
      map((r: StrictHttpResponse<CaffEntity>) => r.body as CaffEntity)
    );
  }

  /**
   * Path part for operation getImgFile
   */
  static readonly GetImgFilePath = '/api/Caff/ImgFile/{id}';

  /**
   * This method provides access to the full `HttpResponse`, allowing access to response headers.
   * To access only the response body, use `getImgFile()` instead.
   *
   * This method doesn't expect any request body.
   */
  getImgFile$Response(params: {
    id: number;
  }): Observable<StrictHttpResponse<void>> {

    const rb = new RequestBuilder(this.rootUrl, CaffService.GetImgFilePath, 'get');
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
   * To access the full response (for headers, for example), `getImgFile$Response()` instead.
   *
   * This method doesn't expect any request body.
   */
  getImgFile(params: {
    id: number;
  }): Observable<void> {

    return this.getImgFile$Response(params).pipe(
      map((r: StrictHttpResponse<void>) => r.body as void)
    );
  }

}
