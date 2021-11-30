import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationDto } from '../api/models';
import { AuthenticationService } from '../api/services';
import { AuthService } from '../authProxy/auth-service.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model:AuthenticationDto={
    userName:"",
    password:""
  }
  submitted = false;
  constructor(private authService:AuthService,private readonly router:Router) { }

  ngOnInit(): void {
  }
  onSubmit(){
    this.authService.register(this.model).subscribe(_=>this.router.navigate(["/login"]))
  }

}
