import { HttpClient, HttpEvent, HttpEventType, HttpParams, HttpRequest, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-file-uploader',
  templateUrl: './file-uploader.component.html',
  styleUrls: ['./file-uploader.component.css']
})
export class FileUploaderComponent implements OnInit {
  file:any[]=[]
  constructor(private http:HttpClient) { }

  ngOnInit(): void {
  }
  onFileChanged(e:any){
    let files:FileList=e.target.files
    if (files.length == 0) {
      console.log("No file selected!");
      return

    }
    let file: File = files[0];

    this.uploadFile("http://localhost:4200/api/Caff/UploadCaffFIle", file)
      .subscribe(
        event => {
          if (event.type == HttpEventType.UploadProgress) {
            //const percentDone = Math.round(100 * event.loaded / event.total);
            //console.log(`File is ${percentDone}% loaded.`);
          } else if (event instanceof HttpResponse) {
            console.log('File is completely loaded!');
          }
        },
        (err) => {
          console.log("Upload Error:", err);
        }, () => {
          console.log("Upload done");
        }
      )
  }
  uploadFile(url:string,file: File):Observable<HttpEvent<unknown>>{
    let formData:FormData=new FormData()
    formData.append('upload',file)
    let params=new HttpParams();
    const options ={
      params:params,
      reportProgress: true
    }
    const req=new HttpRequest('Post',url,formData,options)
    return this.http.request(req)
  }
  onSubmit(){
    console.log(this.file)
  }

}
