import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  constructor(public accountService : AccountService, public router : Router){}
  registerForm = new FormGroup({
    email : new FormControl('',[Validators.required,Validators.email]),
    name : new FormControl('',[Validators.required,Validators.minLength(3)]),
    password : new FormControl('',[Validators.required])
  })

  onSubmit(){
    this.accountService.register(this.registerForm.value).subscribe({
      next:user=> this.router.navigate(['/home']),
      error:err => {console.log(err)}
    })
  }
  get getEmail(){
    return this.registerForm.controls['email'];
  }
  get getName(){
    return this.registerForm.controls['name'];
  }
  get getPassword(){
    return this.registerForm.controls['password'];
  }


}
