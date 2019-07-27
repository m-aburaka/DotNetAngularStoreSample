import { Component, OnInit, Inject } from "@angular/core";
import { Product } from "../../../core/models/Product";
import { ProductsService } from "../../../core/services/products.service";

@Component({
  selector: "app-products",
  templateUrl: "./products.component.html",
  styleUrls: ["../../shared.styles.css"]
})
export class ProductsComponent implements OnInit {
  products: Product[];
  loading = true;
  error: string;

  constructor(private productsService: ProductsService) {}

  ngOnInit() {
    this.update();
  }

  update() {
    this.productsService.get().subscribe(
      products => {
        this.products = products;
        this.loading = false;
      },
      error => (this.error = error.message)
    );
  }
}
