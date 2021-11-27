import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FileUploaderComponent } from './file-uploader/file-uploader.component';
import { FileViewerComponent } from './file-viewer/file-viewer.component';
import { LoginComponent } from './login/login.component';
import { PasswordChangeComponent } from './password-change/password-change.component';
import { RegisterComponent } from './register/register.component';

const routes: Routes = [
  {path:'login',
component : LoginComponent},
{path:'register',
component: RegisterComponent},
{path:'changePassword',component:PasswordChangeComponent},
{path:'',redirectTo:'/login' ,pathMatch:'full' },
{path:'fileupload',component:FileUploaderComponent},
{path:'view',component:FileViewerComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
