import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './account/login/login.component';
import { RegisterComponent } from './account/register/register.component';
import { HomeComponent } from './home/home/home.component';
import { ShopComponent } from './shop/shop/shop.component';
import { ProductFormComponent } from './shop/product-form/product-form.component';
import { UpdateNameComponent } from './account/update-name/update-name.component';

const routes: Routes = [
  {path : '' , component : HomeComponent},
  {path : 'home' , component : HomeComponent},
  {path : 'create' , component : ProductFormComponent},
  {path : 'account/update' , component : UpdateNameComponent},
  {path : 'Edit/:code' , component : ProductFormComponent},
  {path : 'login' , component : LoginComponent},
  {path : 'shop' , component : ShopComponent},
  {path : 'register' , component : RegisterComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
