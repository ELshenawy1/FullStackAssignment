import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from '../shared/models/product';
import { Observable, catchError } from 'rxjs';
import { AccountService } from '../account/account.service';
import { Pagination } from '../shared/models/paging';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl: string = 'https://localhost:7220/api/product';
  constructor(private httpClient : HttpClient, private router : Router, private accountService : AccountService) { }

  GetProducts(shopParams : ShopParams){
    let headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${localStorage.getItem('token')}`);
    return this.httpClient.get<Pagination<Product[]>>(`${this.baseUrl}?sort=${shopParams.sort}&pageIndex=${shopParams.pageNumber}&pageSize=${shopParams.pageSize}${(shopParams.search) ? `&search=${shopParams.search}` : ''}`,{headers});
  }

  GetProductByCode(code : string){
    let headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${localStorage.getItem('token')}`);
    return this.httpClient.get<Product>(`${this.baseUrl}/${code}`,{headers});
  }

  RemoveProduct(code : string){    
    let headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${localStorage.getItem('token')}`);
    return this.httpClient.delete(`${this.baseUrl}?productCode=${code}`,{headers})
  }

  AddProduct(product : FormData){
    let headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${localStorage.getItem('token')}`);
    return this.httpClient.post(this.baseUrl,product,{headers});
  }
  EditProduct(product : FormData){
    let headers = new HttpHeaders();
    headers = headers.set('Authorization',`Bearer ${localStorage.getItem('token')}`);
    return this.httpClient.put(this.baseUrl,product,{headers});
  }
}
