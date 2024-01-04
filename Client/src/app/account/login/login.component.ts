import { Component } from '@angular/core';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  constructor(public accountService : AccountService, public router : Router){}
  loginForm = new FormGroup({
    email : new FormControl('',[Validators.required,Validators.email]),
    password : new FormControl('',[Validators.required])
  })
  invalidEmailAndPassword : boolean = false;

  onSubmit(){
    this.accountService.login(this.loginForm.value).subscribe({
      next:user=> this.router.navigate(['/home']),
      error:err => this.invalidEmailAndPassword = true
    })
  }
  toggle(){
    this.invalidEmailAndPassword = !this.invalidEmailAndPassword;
  }

  get getEmail(){
    return this.loginForm.controls['email'];
  }
  get getPassword(){
    return this.loginForm.controls['password'];
  }
}
