import { Component, OnInit } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';
import { User } from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit{

  currentUser? : User; 
  constructor(public accountService : AccountService){}
  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next:user=>{
        if(user)this.currentUser = user;
        else this.currentUser = undefined
      }
    })
  }
}
