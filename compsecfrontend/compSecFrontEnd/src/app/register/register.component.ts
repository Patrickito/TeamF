import { Component, OnInit } from '@angular/core';
import { AuthenticationDto } from '../api/models';
import { AuthenticationService } from '../api/services';

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
  constructor(private authService:AuthenticationService) { }

  ngOnInit(): void {
  }
  onSubmit(){
    this.authService.register({body:this.model}).subscribe()
  }

}
