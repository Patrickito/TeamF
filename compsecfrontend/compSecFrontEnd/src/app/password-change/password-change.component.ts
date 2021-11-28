import { Component, OnInit } from '@angular/core';
import { PasswordChangeDto } from '../api/models';
import { AuthenticationService } from '../api/services';

@Component({
  selector: 'app-password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.css']
})
export class PasswordChangeComponent implements OnInit {

  model:PasswordChangeDto={
    oldPassword:"",
    newPassword:""
  }
  submitted = false;
  constructor(private authService:AuthenticationService) { }

  ngOnInit(): void {
  }
  onSubmit(){
    this.authService.changePassword({body:this.model}).subscribe(_=>console.log(_))
    console.log(this.model)
  }
}
