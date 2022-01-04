import { AfterViewInit, Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';
import { IOrder } from 'src/app/shared/models/order';
import { CheckoutService } from '../checkout.service';

declare var Stripe;

@Component({
  selector: 'app-checkout-payment',
  templateUrl: './checkout-payment.component.html',
  styleUrls: ['./checkout-payment.component.scss']
})
export class CheckoutPaymentComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() checkoutForm: FormGroup
  @ViewChild('cardNumber', { static: true }) cardNumberElement: ElementRef
  @ViewChild('cardExpiry', { static: true }) cardExpiryElement: ElementRef
  @ViewChild('cardCvc', { static: true }) cardCvcElement: ElementRef
  stripe: any
  cardNumber: any
  cardExpiry: any
  cardCvc: any
  cardErrors: any;
  cardHandler = this.onChange.bind(this)
  loading = false
  cardNumberValid = false
  cardExpiryValid = false
  cardCvcValid = false

  constructor(private basketService: BasketService,
    private checkoutService: CheckoutService,
    private toastr: ToastrService,
    private router: Router) { }


  ngOnDestroy(): void {
    this.cardNumber.destroy()
    this.cardExpiry.destroy()
    this.cardCvc.destroy()
  }

  ngAfterViewInit(): void {
    this.stripe = Stripe('pk_test_51KDsNEFZFOhYzhyzDmyuNVD5TQJzBTcdr1xKprmM46goO0WlgvEAkJDyctSqBsGYvYaZGJLPB9aBPm48RNF1tSJi00wDxqUzaP')
    const elements = this.stripe.elements()

    this.cardNumber = elements.create('cardNumber')
    this.cardNumber.mount(this.cardNumberElement.nativeElement)
    this.cardNumber.addEventListener('change', this.cardHandler)

    this.cardExpiry = elements.create('cardExpiry')
    this.cardExpiry.mount(this.cardExpiryElement.nativeElement)
    this.cardExpiry.addEventListener('change', this.cardHandler)

    this.cardCvc = elements.create('cardCvc')
    this.cardCvc.mount(this.cardCvcElement.nativeElement)
    this.cardCvc.addEventListener('change', this.cardHandler)
  }

  ngOnInit(): void { }

  onChange(event) {
    console.log(event)
    if (event.error) {
      this.cardErrors = event.error.message
    } else {
      this.cardErrors = null
    }

    switch (event.elementType) {
      case 'cardNumber':
        this.cardNumberValid = event.complete
        break;
      case 'cardExpiry':
        this.cardExpiryValid = event.complete
        break; 
      case 'cardCvc':
        this.cardCvcValid = event.complete
        break;
      default:
        break;
    }
  }

  async submitOrder() {
    this.loading = true
    const basket = this.basketService.getCurrentBasketValue()
    try {
      const createdOrder = await this.createOrder(basket)
      const paymentResult = await this.confirmPaymentWithStripe(basket)

      if (paymentResult.paymentIntent) {
        this.basketService.deleteBasket(basket)
        const navigationExtras: NavigationExtras = { state: createdOrder }
        this.router.navigate(['checkout/success'], navigationExtras)
      } else {
        this.toastr.error(paymentResult.error.message)
      }

      this.loading = false
    } catch (error) {
      console.log(error)
      this.toastr.error(error.message)

      this.loading = false
    }
  }

  private async confirmPaymentWithStripe(basket: IBasket) {
    return this.stripe.confirmCardPayment(basket.clientSecret, {
      payment_method: {
        card: this.cardNumber,
        billing_details: {
          name: this.checkoutForm.get('paymentForm').get('nameOnCard').value
        }
      }
    })
  }

  private getOrderToCreat(basket: IBasket) {
    return {
      basketId: basket.id,
      deliveryMethodId: +this.checkoutForm.get('deliveryForm').get('deliveryMethod').value,
      shipToAddress: this.checkoutForm.get('addressForm').value
    }
  }

  private async createOrder(basket: IBasket) {
    const orderToCreat = this.getOrderToCreat(basket)
    return this.checkoutService.creatOrder(orderToCreat).toPromise()
  }
}
