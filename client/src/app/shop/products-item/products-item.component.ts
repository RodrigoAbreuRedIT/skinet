import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/products';

@Component({
  selector: 'app-products-item',
  templateUrl: './products-item.component.html',
  styleUrls: ['./products-item.component.scss']
})
export class ProductsItemComponent implements OnInit {
  @Input("productsFromProducts-item") product: IProduct;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
  }

  addItemToBasket(){
    this.basketService.addItemToBasket(this.product)
  }
}
