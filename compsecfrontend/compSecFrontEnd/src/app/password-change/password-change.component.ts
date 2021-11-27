import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.css']
})
export class PasswordChangeComponent implements OnInit {

  model={
    oldpassword:"",
    newpassword:""
  }
  submitted = false;
  constructor() { }

  ngOnInit(): void {
  }
  onSubmit(){
    console.log(this.model)
  }
}
