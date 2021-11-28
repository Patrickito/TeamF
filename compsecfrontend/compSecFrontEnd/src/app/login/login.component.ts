import { Component, OnInit } from '@angular/core';
import { AuthenticationDto } from '../api/models';
import { AuthenticationService } from '../api/services';

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
  constructor(private authService: AuthenticationService) { }

  ngOnInit(): void {
  }
  onSubmit(){
    this.authService.login({body:this.model}).subscribe()

  }


}