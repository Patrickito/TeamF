import { Component, OnInit } from '@angular/core';
import { CaffEntity, RoleChangeDto, User } from '../api/models';
import { AuthenticationService, CaffService } from '../api/services';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  caffImgs:CaffEntity[]=[]
  users:User[]=[]
  selectedUser:RoleChangeDto={

  }
  constructor(private caffService:CaffService,private authService:AuthenticationService) {
   }

  ngOnInit(): void {
    this.reloadCaffs()
  }
  changeUser(){
    this.authService.changeRoles({body:this.selectedUser})
  }
  RemoveCaff(e:any,img:CaffEntity){
    this.caffService.deleteCaffFile({id:img.id!!}).subscribe(_=>this.reloadCaffs())
    
  }
  reloadCaffs(){
    this.caffService.getAllCaffFiles().subscribe(_=>{this.caffImgs=_;})
  }



}
