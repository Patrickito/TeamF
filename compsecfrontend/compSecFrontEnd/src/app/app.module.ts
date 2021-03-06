import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { PasswordChangeComponent } from './password-change/password-change.component';
import { FileUploaderComponent } from './file-uploader/file-uploader.component';
import { FileViewerComponent } from './file-viewer/file-viewer.component';
import { ApiModule } from './api/api.module';
import { environment } from 'src/environments/environment';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { AuthService } from './authProxy/auth-service.service';
import { AuthInterceptor } from './authProxy/auth.interceptor';
import { Router, RouterModule } from '@angular/router';
import { ImageBrowserComponent } from './image-browser/image-browser.component';
import { MenuComponent } from './menu/menu.component';
import { CommonModule } from '@angular/common';
import { IsAuthenticatedGuard } from './guard/is-authenticated.guard';
import { IsNotAuthenticatedGuard } from './guard/is-not-authenticated.guard';
import { AdminGuard } from './guard/admin.guard';
import { NotfoundComponent } from './notfound/notfound.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    PasswordChangeComponent,
    FileUploaderComponent,
    FileViewerComponent,
    AdminDashboardComponent,
    ImageBrowserComponent,
    MenuComponent,
    NotfoundComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    CommonModule,
    RouterModule,
    AppRoutingModule,
    ApiModule.forRoot({rootUrl:"http://localhost:4200/api"})
  ],
  providers: [AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
      deps: [AuthService, Router],
    },IsAuthenticatedGuard,
    IsNotAuthenticatedGuard,
    AdminGuard,],
  bootstrap: [AppComponent]
})
export class AppModule { }
