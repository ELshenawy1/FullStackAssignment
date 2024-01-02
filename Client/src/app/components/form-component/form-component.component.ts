import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-form-component',
  templateUrl: './form-component.component.html',
  styleUrls: ['./form-component.component.css']
})
export class FormComponentComponent {
  constructor(private httpClient : HttpClient){}
  selectedFile?: File;
  onFileChanged(event : any){
    this.selectedFile = event.target.files[0];
  }
  onUpload(){
    const formData = new FormData();
    formData.append('file', this.selectedFile!);

    this.httpClient.post('https://localhost:7220/api/Test/upload', formData)
      .subscribe(response => {
        console.log(response);
      });
  }
}
