import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-paging-header',
  templateUrl: './paging-header.component.html',
  styleUrls: ['./paging-header.component.scss']
})
export class PagingHeaderComponent implements OnInit {
  
  @Input('pageNumberFrompagingHeader') pageNumber: number;
  @Input('pageSizeFrompagingHeader') pageSize: number;
  @Input('totalCountFrompagingHeader') totalCount: number;


  constructor() { }

  ngOnInit(): void {
  }

}
