import { Component, OnInit } from "@angular/core";
import { FormControl } from "@angular/forms";
import { startWith, map } from "rxjs/operators";
import { Observable } from "rxjs";
import { ProductsService } from "../../../core/services/products.service";
import { Product } from "../../../core/models/Product";

@Component({
  selector: "app-customer-purchase-new",
  templateUrl: "./customer-purchase-new.component.html",
  styleUrls: ["../../shared.styles.css"]
})
export class CustomerPurchaseNewComponent implements OnInit {
  myControl = new FormControl();
  products: Product[];
  filteredProducts: Observable<Product[]>;

  constructor(private productsService: ProductsService) {}

  ngOnInit() {
    this.productsService
      .get()
      .subscribe(products => (this.products = products));

    this.filteredProducts = this.myControl.valueChanges.pipe(
      startWith(""),
      map(value => this._filter(value))
    );
  }

  private _filter(value: string): Product[] {
    if (!this.products) {
      return;
    }

    const filterValue = value.toLowerCase();

    return this.products.filter(product =>
      product.name.toLowerCase().includes(filterValue)
    );
  }

  onAddClick() {}
}
