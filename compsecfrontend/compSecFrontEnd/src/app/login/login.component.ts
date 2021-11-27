import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationDto } from '../api/models';
import { AuthenticationService } from '../api/services';
import { AuthService } from '../authProxy/auth-service.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model:AuthenticationDto={
    userName:"",
    password:""
  }
  submitted = false;
  constructor(private authService: AuthService,private router:Router) { }

  ngOnInit(): void {
  }
  onSubmit(){
    this.authService.login(this.model).subscribe(a=>this.router.navigate(["/admin"]))

  }


}
