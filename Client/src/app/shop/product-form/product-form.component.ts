import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ShopService } from '../shop.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit{
  productCode? : string|null ;
  constructor(private shopService : ShopService,
    private router : Router, 
    private activatedRoute : ActivatedRoute){}
  ngOnInit(): void {
    
    this.productCode = this.activatedRoute.snapshot.paramMap.get("code");
    if(this.productCode){
      this.shopService.GetProductByCode(this.productCode).subscribe({
        next:product=>{
          this.productForm.get('productCode')?.setValue(product.productCode);
          this.productForm.get('name')?.setValue(product.name);
          this.productForm.get('price')?.setValue(product.price.toString());
          this.productForm.get('category')?.setValue(product.category);
          this.productForm.get('category')?.setValue(product.category);
          this.productForm.get('minQuantity')?.setValue(product.minQuantity.toString());
          this.productForm.get('discountRate')?.setValue(product.discountRate.toString());
        }
      });

    }
  }
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
    img : new FormControl(''),
    minQuantity : new FormControl('',[Validators.required]),
    discountRate : new FormControl('',[Validators.required]),
  })

  formOperation(e : any){
    const formData = new FormData();
    formData.append('productCode', this.productForm.get('productCode')!.value!);
    formData.append('name', this.productForm.get('name')!.value!);
    formData.append('price', this.productForm.get('price')!.value!);
    formData.append('category', this.productForm.get('category')!.value!);
    formData.append('img', this.selectedFile!);
    formData.append('minQuantity', this.productForm.get('minQuantity')!.value!);
    formData.append('discountRate', this.productForm.get('discountRate')!.value!);

    if(this.productCode){
      this.shopService.EditProduct(formData).subscribe({
        next:(response)=>{console.log(response);this.router.navigate(['/shop'])},
        error:(error)=>{console.log(error);console.log(this.productForm.value)}
      })
    }else{
      this.shopService.AddProduct(formData).subscribe({
        next:(response)=>{console.log(response);this.router.navigate(['/shop'])},
        error:(error)=>{console.log(error);console.log(this.productForm.value)}
      })
    }

  }
}
