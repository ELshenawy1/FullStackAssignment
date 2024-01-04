import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { CarouselModule} from 'ngx-bootstrap/carousel'
import { PaginationModule } from 'ngx-bootstrap/pagination';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedRoutingModule,
    CarouselModule.forRoot(),
    PaginationModule.forRoot()
  ],
  exports:[
    CarouselModule,
    PaginationModule
  ]
})
export class SharedModule { }
