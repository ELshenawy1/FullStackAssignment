import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UpdateNameComponent } from './update-name/update-name.component';


@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    UpdateNameComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    ReactiveFormsModule
  ],
  exports:[
    LoginComponent,
    RegisterComponent
  ]
})
export class AccountModule { }
