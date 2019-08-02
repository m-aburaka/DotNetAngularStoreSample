import { Component, OnInit, Input } from "@angular/core";
import { Product } from "../../../core/models/Product";
import { ProductsService } from "./../../../core/services/products.service";

@Component({
  selector: "app-product",
  templateUrl: "./product.component.html",
  styleUrls: ["../../shared.styles.css"]
})
export class ProductComponent implements OnInit {
  @Input() product: Product;

  constructor(private productsService: ProductsService) {}

  ngOnInit() {}

  onDeleteClick() {
    this.productsService.delete(this.product);
  }
}
