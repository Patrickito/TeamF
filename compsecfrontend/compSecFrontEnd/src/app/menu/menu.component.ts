import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../api/services';
import { AuthService } from '../authProxy/auth-service.service';
import jwt_decode from 'jwt-decode'
@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  currentRoute:string=""
  isAdmin=false
  isLoggedIn=false;
  constructor(private router:Router,private authService:AuthService) { }

  ngOnInit(): void {
    const token = this.authService.getToken();
    if (token === null) {
      this.isAdmin= false;
    
    }
    else{
    const decodedToken = jwt_decode<any>(token!!);
    if (!decodedToken) {
      this.isAdmin= false;
    }
    else{
      this.isLoggedIn=true;
    const role = decodedToken.role;
    console.log(decodedToken)
    this.isAdmin=role.includes("Administrator")
    }
  }
    console.log(this.isAdmin)
    this.currentRoute=this.router.url
  }

}
