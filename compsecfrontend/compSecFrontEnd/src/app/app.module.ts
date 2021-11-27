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
import { HttpClientModule } from '@angular/common/http';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    PasswordChangeComponent,
    FileUploaderComponent,
    FileViewerComponent,
    AdminDashboardComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    AppRoutingModule,
    ApiModule.forRoot({rootUrl:"http://localhost:4200/api"})
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
