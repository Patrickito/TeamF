import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-file-uploader',
  templateUrl: './file-uploader.component.html',
  styleUrls: ['./file-uploader.component.css']
})
export class FileUploaderComponent implements OnInit {
  file:any[]=[]
  constructor() { }

  ngOnInit(): void {
  }
  onFileChanged(e:any){
    this.file=e.target.files
  }
  onSubmit(){
    console.log(this.file)
  }

}
