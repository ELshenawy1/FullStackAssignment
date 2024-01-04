import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  constructor(private accoutnService : AccountService){}
  ngOnInit(): void {
    this.accoutnService.RefreshToken();
    setInterval(() => {
      if(new Date(localStorage.getItem('tokenExpiration')!) < new Date()){
        this.accoutnService.RefreshToken();
        console.log("Refresh Token Now")
      }
    }, 6000);
  }
  title = 'eCommerce';
}
