import { CursorError } from '@angular/compiler/src/ml_parser/lexer';
import { Component, OnInit } from '@angular/core';
import { CaffEntity, RoleChangeDto, User, UserDto } from '../api/models';
import { AuthenticationService, CaffService } from '../api/services';
import { AuthService } from '../authProxy/auth-service.service';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  caffImgs:CaffEntity[]=[]
  users:UserDto[]=[]
  allRoles:string[]=[]
  currentUser=""
  constructor(private caffService:CaffService,private authService:AuthenticationService,private tokenService:AuthService) {
   }

  ngOnInit(): void {
    this.reloadCaffs()
    this.reloadUsers()
    this.currentUser=this.tokenService.getUserName()!!
  }
  RemoveCaff(e:any,img:CaffEntity){
    this.caffService.deleteCaffFile({id:img.id!!}).subscribe(_=>this.reloadCaffs())
  }
  removeUser(e:any,user:UserDto){
    this.authService.deleteUser({id:user.id!!}).subscribe(_=>this.reloadUsers())
  }
  reloadUsers(){
    this.authService.getAllUsers().subscribe(_=>this.users=_)
    this.authService.getAllRoles().subscribe(_=>this.allRoles=_)
  }
  reloadCaffs(){
    this.caffService.getAllCaffFiles().subscribe(_=>{this.caffImgs=_;console.log(_)})
  }
  addRole(e:any,user:UserDto,role:string){
    let newRole:string[]=user.roles!!
    newRole[newRole.length]=role
    let roleDto={roles:newRole,userName:user.userName!!}
    this.authService.changeRoles({body:roleDto}).subscribe()
  }
  removeRole(e:any,user:UserDto,role:string){
    let newRole:string[]=[]
    for(let i=0;i<user.roles!!.length;i++){
      if(user.roles!![i].toUpperCase()!=role.toUpperCase()){
        newRole[i]=user.roles!![i]
      }
    }
    let roleDto:RoleChangeDto ={roles:newRole,userName:user.userName!!}
    this.authService.changeRoles({body:roleDto}).subscribe(_=> this.reloadUsers())
    

  }


}
