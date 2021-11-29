import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminDashboardComponent } from './admin-dashboard/admin-dashboard.component';
import { FileUploaderComponent } from './file-uploader/file-uploader.component';
import { FileViewerComponent } from './file-viewer/file-viewer.component';
import { AdminGuard } from './guard/admin.guard';
import { IsAuthenticatedGuard } from './guard/is-authenticated.guard';
import { IsNotAuthenticatedGuard } from './guard/is-not-authenticated.guard';
import { ImageBrowserComponent } from './image-browser/image-browser.component';
import { LoginComponent } from './login/login.component';
import { PasswordChangeComponent } from './password-change/password-change.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {path:'login',
component : LoginComponent},
{path:'register',
component: RegisterComponent},
{path:'changePassword',component:PasswordChangeComponent},


{path:'fileupload',component:FileUploaderComponent,canActivate:[IsAuthenticatedGuard]},
{path:'details/:caffId',component:FileViewerComponent,canActivate:[IsAuthenticatedGuard]},
{path:'admin',component:AdminDashboardComponent,canActivate:[IsAuthenticatedGuard,AdminGuard]},
{path:'browse',component:ImageBrowserComponent,canActivate:[IsAuthenticatedGuard]},
{path:'',redirectTo:'/login' ,pathMatch:'full',},
{path:'**', redirectTo:'/login',pathMatch:'full'},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
