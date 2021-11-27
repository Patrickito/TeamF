import { Component, OnInit } from '@angular/core';
import { CaffEntity } from '../api/models';
import { CaffService } from '../api/services';

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  caffImgs:CaffEntity[]=[]
  constructor(private caffService:CaffService) {
    
   }

  ngOnInit(): void {
    this.caffService.getAllCaffFiles().subscribe(_=>this.caffImgs=_)
  }

}
