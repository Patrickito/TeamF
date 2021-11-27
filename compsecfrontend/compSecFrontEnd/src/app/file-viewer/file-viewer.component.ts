import { Component, OnInit } from '@angular/core';
import * as saveAs from 'file-saver';
import { CaffEntity, Comment } from '../api/models';
import { CaffService, CommentService } from '../api/services';

@Component({
  selector: 'app-file-viewer',
  templateUrl: './file-viewer.component.html',
  styleUrls: ['./file-viewer.component.css']
})
export class FileViewerComponent implements OnInit {
  model:Comment={
    commentText:""
  }
  model2:CaffEntity={
    
  }
path:string=""
  constructor(private commentService:CommentService,private caffService:CaffService) { }

  ngOnInit(): void {
    this.path="/assets/response.jpg"
  }
  onSubmit(){
    this.commentService.addCaffFileComment({body:this.model})
  }
  downloadCaff(){
    this.caffService.getImgFile({id: this.model2.id||0})
  }

}
