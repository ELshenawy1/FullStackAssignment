import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedRoutingModule } from './shared-routing.module';
import { CarouselModule} from 'ngx-bootstrap/carousel'

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedRoutingModule,
    CarouselModule.forRoot()
  ],
  exports:[
    CarouselModule
  ]
})
export class SharedModule { }
