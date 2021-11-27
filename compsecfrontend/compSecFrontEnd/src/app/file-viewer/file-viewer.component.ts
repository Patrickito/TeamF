import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-file-viewer',
  templateUrl: './file-viewer.component.html',
  styleUrls: ['./file-viewer.component.css']
})
export class FileViewerComponent implements OnInit {
  model={
    comment:""
  }
path:string=""
  constructor() { }

  ngOnInit(): void {
    this.path="/assets/response.jpg"
  }
  onSubmit(){
    console.log(this.model)
  }

}
