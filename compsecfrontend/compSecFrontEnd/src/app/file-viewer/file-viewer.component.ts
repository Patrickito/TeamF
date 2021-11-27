import { Component, OnInit } from '@angular/core';
import * as saveAs from 'file-saver';
import { CaffEntity, Comment } from '../api/models';
import { CaffService, CommentService } from '../api/services';
import {Observable} from 'rxjs'
import { HttpClient } from '@angular/common/http';

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
  constructor(private commentService:CommentService,private caffService:CaffService,private http:HttpClient) { }
  getImage(imageUrl: string): Observable<Blob> {
    return this.http.get(imageUrl, { responseType: 'blob' });
  }
  createImageFromBlob(image: Blob) {
    let reader = new FileReader();
    reader.addEventListener("load", () => {
       this.path=reader.result!!.toString()
    }, false);
 
    if (image) {
       reader.readAsDataURL(image);
    }
  }
  ngOnInit(): void {
      this.getImage("http://localhost:4200/api/api/Caff/imgfile/1").subscribe(_=>this.createImageFromBlob(_))
  }
  onSubmit(){
    this.commentService.addCaffFileComment({body:this.model}).subscribe()
  }
  downloadCaff(){
    this.caffService.getImgFile({id: this.model2.id||0}).subscribe()
  }
}
