import { Component, OnInit } from '@angular/core';
import * as saveAs from 'file-saver';
import { CaffEntity, Comment } from '../api/models';
import { CaffService, CommentService } from '../api/services';
import {Observable} from 'rxjs'
import { HttpClient } from '@angular/common/http';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-file-viewer',
  templateUrl: './file-viewer.component.html',
  styleUrls: ['./file-viewer.component.css']
})
export class FileViewerComponent implements OnInit {
  model:Comment={
    caffEntityId:1,
    commentText:""
  }
  model2:CaffEntity={
    
  }
  comments:Comment[]=[]
path:SafeUrl=""
private caffId:number=0
  constructor(private commentService:CommentService,private caffService:CaffService,private http:HttpClient,private sanitizer:DomSanitizer, activeRoute:ActivatedRoute) {
    this.caffId=activeRoute.snapshot.params["caffId"]
  }
  getImage(imageUrl: string): Observable<Blob> {
    return this.http.get(imageUrl, { responseType: 'blob' });
  }
  createImageFromBlob(image: Blob){
    let reader = new FileReader();
    reader.addEventListener("load", () => {
       this.path=this.sanitizer.bypassSecurityTrustUrl(reader.result!!.toString())
    }, false);
 
    if (image) {
       reader.readAsDataURL(image);
    }
  }
  ngOnInit(): void {
    
      this.caffService.getCaffFile({id:this.caffId}).subscribe(_=>console.log(_))
      this.commentService.getCaffFileComment({id:this.caffId}).subscribe(_=>{this.comments=_; console.log(this.comments)})
      
      this.getImage("http://localhost:4200/api/api/Caff/imgfile/1").subscribe(_=>this.createImageFromBlob(_))
  }
  onSubmit(){
    this.commentService.addCaffFileComment({body:this.model}).subscribe()
  }
  downloadCaff(){
    this.caffService.getImgFile({id: this.model2.id||1}).subscribe()
  }
}
