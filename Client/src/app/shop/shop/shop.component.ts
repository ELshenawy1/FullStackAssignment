import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ShopService } from '../shop.service';
import { Product } from 'src/app/shared/models/product';
import { ShopParams } from 'src/app/shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit{
  sortOptions=[
    {name : 'Alphabetical' , value :''},
    {name : 'Price : Low to High' , value :'priceasc'},
    {name : 'Price : High to Low' , value :'pricedesc'}
  ]
  @ViewChild('search') search ?: ElementRef;
  products : Product[] = [];
  totalCount= 0;
  shopParams = new ShopParams();
  constructor(private shopService:ShopService){}
  ngOnInit(): void {
    this.GetProducts()
  }

  GetProducts(){
    this.shopService.GetProducts(this.shopParams).subscribe({
      next:(res)=>{
        this.products = res.data;
        this.shopParams.pageNumber = res.pageIndex
        this.shopParams.pageSize = res.pageSize
        this.totalCount = res.count
      }})
  }

  filterProducts(code : any){
    this.products = this.products.filter(p=> p.productCode != code);
  }

  onPageChanging(event : any){
    if(this.shopParams.pageNumber!==event.page){
      this.shopParams.pageNumber = event.page
      this.GetProducts()
    }
  }

  onSortSelected(event : any){
    this.shopParams.sort = event.target.value;
    this.GetProducts()
  }

  onSearch(){
    this.shopParams.search = this.search?.nativeElement.value;
    this.GetProducts();
  }

  onReset(){
    if(this.search) this.search.nativeElement.value = ''
    this.shopParams.search = '';
    this.GetProducts();
  }
}
