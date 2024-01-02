import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { Product } from 'src/app/shared/models/product';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit{
  products : Product[] = [];

  constructor(private shopService:ShopService){}
  ngOnInit(): void {
    this.shopService.GetProducts().subscribe({
      next:(res)=>{
        this.products = res.data;
      }})
  }

  filterProducts(code : any){
    this.products = this.products.filter(p=> p.productCode != code);
  }
}
