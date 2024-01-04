import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShopRoutingModule } from './shop-routing.module';
import { ProductItemComponent } from './product-item/product-item.component';
import { ShopComponent } from './shop/shop.component';
import { ProductFormComponent } from './product-form/product-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    ProductItemComponent,
    ShopComponent,
    ProductFormComponent
  ],
  imports: [
    CommonModule,
    ShopRoutingModule,
    ReactiveFormsModule,
    SharedModule
  ],
  exports:[
    ShopComponent
  ]
})
export class ShopModule { }
