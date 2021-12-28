import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyResquestCount = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  busy(){
    this.busyResquestCount++;
    this.spinnerService.show(undefined, {
      type: 'ball-atom',
      bdColor: 'rgba(255, 255, 255, 0.7)',
      color: '#333333'
    })
  }

  idle(){
    this.busyResquestCount--;
    if(this.busyResquestCount <= 0){
      this.busyResquestCount = 0
      this.spinnerService.hide();
    }
  }
}
