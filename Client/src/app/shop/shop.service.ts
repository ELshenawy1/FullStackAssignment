import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from '../shared/models/product';
import { Observable, catchError } from 'rxjs';
import { AccountService } from '../account/account.service';
import { Pagination } from '../shared/models/paging';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl: string = 'https://localhost:7220/api/product';
  constructor(private httpClient : HttpClient, private router : Router, private accountService : AccountService) { }

  GetProducts(){
    return this.httpClient.get<Pagination<Product[]>>(this.baseUrl);
  }

  RemoveProduct(code : string){
    console.log(code)
    return this.httpClient.delete(`${this.baseUrl}?productCode=${code}`)
  }

  AddProduct(product : any){
    return this.httpClient.post(this.baseUrl,product);
  }
}
