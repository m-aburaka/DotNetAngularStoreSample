import { Component, OnInit, Input } from "@angular/core";
import {
  FormControl,
  Validators,
  AbstractControl,
  ValidationErrors
} from "@angular/forms";
import { startWith, map } from "rxjs/operators";
import { Observable } from "rxjs";
import { ProductsService } from "../../../core/services/products.service";
import { Product } from "../../../core/models/Product";
import { CustomerPurchasesService } from "./../../../core/services/customer-purchases.service";
import { Customer } from "src/app/core/models/Customer";

@Component({
  selector: "app-customer-purchase-new",
  templateUrl: "./customer-purchase-new.component.html",
  styleUrls: ["../../shared.styles.css"]
})
export class CustomerPurchaseNewComponent implements OnInit {
  @Input() customer: Customer;
  productControl = new FormControl("", [
    Validators.required,
    this.validateProduct.bind(this)
  ]);
  products: Product[];
  filteredProducts: Observable<Product[]>;
  selectedProduct: Product;

  constructor(
    private productsService: ProductsService,
    private customerPurchasesService: CustomerPurchasesService
  ) {}

  ngOnInit() {
    this.productsService
      .getAll()
      .subscribe(products => (this.products = products));

    this.filteredProducts = this.productControl.valueChanges.pipe(
      startWith(""),
      map(value => this._filter(value))
    );
  }

  validateProduct(control: AbstractControl): ValidationErrors {
    if (this.selectedProduct) {
      return null;
    } else {
      return {
        validateProduct: {
          valid: false
        }
      };
    }
  }

  onNameChange(value: string) {
    if (this.selectedProduct && this.selectedProduct.name !== value) {
      this.selectedProduct = null;
      this.productControl.updateValueAndValidity();
    }
  }

  private _filter(value: string | Product): Product[] {
    if (!this.products) {
      return;
    }

    let filterValue: string;
    if (!value) {
      filterValue = "";
    } else if (typeof value === "string") {
      filterValue = value.toLowerCase();
    } else {
      filterValue = value.name.toLowerCase();
    }

    return this.products.filter(p =>
      p.name.toLowerCase().includes(filterValue)
    );
  }

  onEnterKeyDown() {
    this.onAddClick();
  }

  onAddClick() {
    if (this.selectedProduct) {
      this.customerPurchasesService.add(this.customer, this.selectedProduct);
      this.selectedProduct = null;
      this.productControl.reset();
      this.productControl.markAsUntouched();
    }
  }

  onProductSelected(event) {
    this.selectedProduct = event.value;
    this.productControl.updateValueAndValidity();
  }

  nameDisplayFn(state: string | Product) {
    if (!state) {
      return "";
    } else {
      return typeof state === "string" ? state : state.name;
    }
  }
}
