import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css']
})
export class ProductItemComponent {
  constructor(private shopService : ShopService, private router : Router){}
  @Input() product? : Product;
  @Output() productRemoved = new EventEmitter();
  removeItem(code : string){
    this.shopService.RemoveProduct(code).subscribe({
      next:()=>{console.log("Removed Successfully")},
      error:()=>{console.log("An Error Occurd")}
    });
    this.productRemoved.emit(code);
  }
  editProduct(code : string){
    this.router.navigate([`/Edit/${code}`])
  }
}
