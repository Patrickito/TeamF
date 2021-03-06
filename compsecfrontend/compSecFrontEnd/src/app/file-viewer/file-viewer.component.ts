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
  fileName=""
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
  caffEntity:CaffEntity={}
  ngOnInit(): void {
      var imgId=0
      this.caffService.getAllCaffFiles().subscribe(_=>{this.caffEntity=_[this.caffId];this.fileName=_[this.caffId].images!![0].caption!!;imgId=_[this.caffId].images!![0].id!!;this.getImage("http://localhost:4200/api/api/Caff/imgfile/"+imgId).subscribe(_=>this.createImageFromBlob(_))})
      this.commentService.getCaffFileComment({id:this.caffId}).subscribe(_=>{this.comments=_; console.log(this.comments)})
      
      
  }
  onSubmit(){
    this.commentService.addCaffFileComment({body:this.model}).subscribe()
  }
  downloadCaff(){
    this.http.get("http://localhost:4200/api/api/Caff/CaffFile/"+this.caffEntity.id,{ responseType: 'blob' as 'json'}).subscribe((response: any) =>{
      let dataType = response.type;
      let binaryData = [];
      binaryData.push(response);
      let downloadLink = document.createElement('a');
      downloadLink.href = window.URL.createObjectURL(new Blob(binaryData, {type: dataType}));
      let filename="temp.caff"
      if (filename)
          downloadLink.setAttribute('download', filename);
      document.body.appendChild(downloadLink);
      downloadLink.click();
  }
)
  }
}
