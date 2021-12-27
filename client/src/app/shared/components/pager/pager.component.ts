import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {
  @Input('totalCountFromPager') totalCount: number;
  @Input('pageSizeFromPager') pageSize: number;

  // emite algo para o componente Pai
  @Output('pageChangedFromPager') pageChanged = new EventEmitter<number>();


  constructor() { }

  ngOnInit(): void {
  }

  onPagerChange(event: any){
    this.pageChanged.emit(event.page);
  }
}
