import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../shared/models/user';
import { BehaviorSubject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl: string = 'https://localhost:7220/api/Account';

  private currentUserSource = new BehaviorSubject<User|null>(null);
  currentUser$ = this.currentUserSource.asObservable();


  constructor(private httpClient : HttpClient, private router : Router) { }

  login(values : any){
    return this.httpClient.post<User>(`${this.baseUrl}/login`, values).pipe(
      tap(user =>{
        localStorage.setItem('token' , user.token);
        localStorage.setItem('refreshToken' , user.refreshToken);
        localStorage.setItem('tokenExpiration' , user.tokenExpiration);
        this.currentUserSource.next(user);
      })
    )

  }



  GetCurrentUserSource(){
    return this.currentUserSource.value;
  }

  register(values : any){
    console.log(values)
    return this.httpClient.post<User>(`${this.baseUrl}/Register`, values).pipe(
      tap(user =>{
        localStorage.setItem('token' , user.token);
        localStorage.setItem('refreshToken' , user.refreshToken);
        localStorage.setItem('tokenExpiration' , user.tokenExpiration);
        this.currentUserSource.next(user);
      })
    )
  }

  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('tokenExpiration');
    this.currentUserSource.next(null);
    console.log(this.currentUser$)
    this.router.navigate(['/'])
  }

}
