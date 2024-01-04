import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-update-name',
  templateUrl: './update-name.component.html',
  styleUrls: ['./update-name.component.css']
})
export class UpdateNameComponent implements OnInit{
  updateUserForm = new FormGroup({
    email : new FormControl(''),
    name : new FormControl('',[Validators.required,Validators.minLength(3)]),
  })

  constructor(private accountService : AccountService, private router : Router){}
  ngOnInit(): void {
    this.accountService.currentUser$.subscribe({
      next:user=>{
        console.log(user)
        this.useremail = user?.email;
      }
    })
  }
  useremail? : string;

  onSubmit(){
    this.updateUserForm.get('email')?.setValue(this.useremail!);
    this.accountService.updateUser(this.updateUserForm.value).subscribe({
      next:user=> {this.router.navigate(['/home']);
    this.accountService.RefreshToken()},
      error:err => {console.log(err)}
    })
  }

}
