import { Component, OnInit } from '@angular/core';
import { CaffEntity, RoleChangeDto, User, UserDto } from '../api/models';
import { AuthenticationService, CaffService } from '../api/services';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  caffImgs:CaffEntity[]=[]
  users:UserDto[]=[]
  selectedUser:RoleChangeDto={

  }
  constructor(private caffService:CaffService,private authService:AuthenticationService) {
   }

  ngOnInit(): void {
    this.reloadCaffs()
    this.reloadUsers()
  }
  changeUser(){
    this.authService.changeRoles({body:this.selectedUser})
  }
  RemoveCaff(e:any,img:CaffEntity){
    this.caffService.deleteCaffFile({id:img.id!!}).subscribe(_=>this.reloadCaffs())
  }
  RemoveUser(e:any,user:UserDto){
  }
  reloadUsers(){
    this.authService.getAllUsers().subscribe(_=>this.users=_)
  }
  reloadCaffs(){
    this.caffService.getAllCaffFiles().subscribe(_=>{this.caffImgs=_;console.log(_)})
  }
  removeRole(e:any,user:UserDto,role:string){
    let newRole:string[]=[]
    for(let i=0;i<user.roles!!.length;i++){
      if(user.roles!![i].toUpperCase()===role.toUpperCase()){
        newRole[i]=user.roles!![i]
      }
    }
    let roleDto:RoleChangeDto ={roles:newRole,userName:user.userName!!}
    this.authService.changeRoles({body:roleDto}).subscribe()
    

  }


}
