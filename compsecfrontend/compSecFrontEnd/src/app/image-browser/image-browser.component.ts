import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { CaffEntity } from '../api/models';
import { CaffService } from '../api/services';
import {Observable} from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-image-browser',
  templateUrl: './image-browser.component.html',
  styleUrls: ['./image-browser.component.css']
})
export class ImageBrowserComponent implements OnInit {

  caffImgs:{caffEntity:CaffEntity,img:SafeUrl} []=[]
  constructor(private caffService:CaffService,private http:HttpClient,private sanitizer:DomSanitizer) { }
  getImage(imageUrl: string): Observable<Blob> {
    return this.http.get(imageUrl, { responseType: 'blob' });
  }
  createImageFromBlob(image: Blob,i:number){
    let reader = new FileReader();
    reader.addEventListener("load", () => {
       this.caffImgs[i].img=this.sanitizer.bypassSecurityTrustUrl(reader.result!!.toString())
    }, false);
 
    if (image) {
       reader.readAsDataURL(image);
    }
  }
  ngOnInit(): void {
    this.caffService.getAllCaffFiles().subscribe(_=>{for(let i=0;i<_.length;i++){
      this.caffImgs[i]={caffEntity:_[i],img:""};
      
    }
    for(let i=0;i<this.caffImgs.length;i++){
      this.getImage("http://localhost:4200/api/api/Caff/imgFile/"+this.caffImgs[i].caffEntity.images!![0]!!.id).subscribe(_=>this.createImageFromBlob(_,i))
    }
  })
   
  }

}
