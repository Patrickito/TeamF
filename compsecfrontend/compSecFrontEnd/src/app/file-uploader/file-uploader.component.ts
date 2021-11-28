import { HttpClient, HttpEvent, HttpEventType, HttpParams, HttpRequest, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-file-uploader',
  templateUrl: './file-uploader.component.html',
  styleUrls: ['./file-uploader.component.css']
})
export class FileUploaderComponent implements OnInit {
  file:File|null=null
  constructor(private http:HttpClient) { }

  ngOnInit(): void {
  }
  onFileChanged(e:any){
    let files:FileList=e.target.files
    if (files.length == 0) {
      console.log("No file selected!");
      return

    }
    this.file = files[0];
  }
  uploadFile(url:string,file: File):Observable<Object>{
    let formData:FormData=new FormData()
    formData.append('1',file)
    let params=new HttpParams();
    return this.http.post(url,formData)
  }
  onSubmit(){
    this.uploadFile("http://localhost:4200/api/api/Caff/", this.file!!)
      .subscribe()
  }

}
