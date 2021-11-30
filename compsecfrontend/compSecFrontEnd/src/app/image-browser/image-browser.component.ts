import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { CaffEntity } from '../api/models';
import { CaffService } from '../api/services';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-image-browser',
  templateUrl: './image-browser.component.html',
  styleUrls: ['./image-browser.component.css']
})
export class ImageBrowserComponent implements OnInit {
  searchParam: string = "Beautiful scenery"
  caffImgs: { caffEntity: CaffEntity, img: SafeUrl }[] = []
  constructor(private caffService: CaffService, private http: HttpClient, private sanitizer: DomSanitizer,private readonly router:Router) { }
  getImage(imageUrl: string): Observable<Blob> {
    return this.http.get(imageUrl, { responseType: 'blob' });
  }
  createImageFromBlob(image: Blob, i: number) {
    let reader = new FileReader();
    reader.addEventListener("load", () => {
      this.caffImgs[i].img = this.sanitizer.bypassSecurityTrustUrl(reader.result!!.toString())
    }, false);

    if (image) {
      reader.readAsDataURL(image);
    }
  }
  ngOnInit(): void {
    this.CreateCaffs()

  }
  CreateCaffs(){
    this.caffImgs=[]
    this.caffService.getAllCaffFiles().subscribe(inputdata => {
      for (let i = 0; i < inputdata.length; i++) {
        if (this.searchParam === "") {
          this.caffImgs[i] = { caffEntity: inputdata[i], img: "" }
        }
        else {
          for (let j = 0; j < inputdata[i].images!!.length; j++) {
            if (inputdata[i].images!![j].caption!!.toUpperCase().includes(this.searchParam.toUpperCase())||inputdata[i].images!![j].tags!!.findIndex(_=>_.tagName?.toUpperCase()===this.searchParam.toUpperCase())!=-1) {
              this.caffImgs[i]={caffEntity:inputdata[i],img:""}
            }
          }
        }


      }
      for (let i = 0; i < this.caffImgs.length; i++) {
        this.getImage("http://localhost:4200/api/api/Caff/imgFile/" + this.caffImgs[i].caffEntity.images!![0]!!.id).subscribe(_ => this.createImageFromBlob(_, i))
      }
    })
  }
  NavigateDetails(e:any,caffImg:CaffEntity){
    this.router.navigate(["/details/"+this.caffImgs.findIndex(caff=>caff.caffEntity.id===caffImg.id)])
  }
  

}
