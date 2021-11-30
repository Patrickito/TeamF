import { HttpClient, HttpErrorResponse, HttpEvent, HttpEventType, HttpParams, HttpRequest, HttpResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { EMPTY, Observable, throwError } from 'rxjs';
import { catchError, map, retry } from 'rxjs/operators';
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
      return
    }
    this.file = files[0];
  }
  uploadFile(url:string,file: File){
    let formData:FormData=new FormData()
    formData.append('1',file)
    this.http.post(url,formData).subscribe(_=>alert("Sikeres fájlfeltöltés"))
  }
  onSubmit(){
    this.uploadFile("http://localhost:4200/api/api/Caff/", this.file!!)
  }
  
 
}
