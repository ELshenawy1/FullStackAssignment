import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent {
  constructor(private shopService : ShopService){}
  selectedFile: File | null = null;
  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0] as File;
    console.log(this.selectedFile)
    console.log(this.productForm.get('img'));
  }
  productForm = new FormGroup({
    productCode : new FormControl('',[Validators.required]),
    name : new FormControl('',[Validators.required]),
    price : new FormControl('',[Validators.required]),
    category : new FormControl('',[Validators.required]),
    img : new FormControl('',[Validators.required]),
    minQuantity : new FormControl('',[Validators.required]),
    discountRate : new FormControl('',[Validators.required]),
  })

  formOperation(e : any){
    this.shopService.AddProduct(this.productForm.value).subscribe({
      next:(response)=>{console.log(response)},
      error:(error)=>{console.log(error);console.log(this.productForm.value)}
    })
  }
}
