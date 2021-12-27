import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/products';

@Component({
  selector: 'app-products-item',
  templateUrl: './products-item.component.html',
  styleUrls: ['./products-item.component.scss']
})
export class ProductsItemComponent implements OnInit {
  @Input("productsFromProducts-item") product: IProduct;

  constructor() { }

  ngOnInit(): void {
  }

}
