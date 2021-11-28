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
      var imgId=0
      this.caffService.getAllCaffFiles().subscribe(_=>{imgId=_[this.caffId].images!![0].id!!;this.getImage("http://localhost:4200/api/api/Caff/imgfile/"+imgId).subscribe(_=>this.createImageFromBlob(_))})
      //this.caffService.getCaffFile({id:this.caffId}).subscribe(_=>console.log(_))
      this.commentService.getCaffFileComment({id:this.caffId}).subscribe(_=>{this.comments=_; console.log(this.comments)})
      
      
  }
  onSubmit(){
    this.commentService.addCaffFileComment({body:this.model}).subscribe()
  }
  downloadCaff(){
    this.caffService.getCaffFile({id:this.caffId}).subscribe(_=>{console.log(_);var name = 'myfile.caff';
    let blob = new Blob([String(_)], {type: "text/plain"});
    saveAs(blob, name);})
  }
}
