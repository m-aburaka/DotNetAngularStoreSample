import { Component, OnInit, Input } from "@angular/core";
import { Product } from "../../../core/models/Product";

@Component({
  selector: "app-product",
  templateUrl: "./product.component.html",
  styleUrls: ["../../shared.styles.css"]
})
export class ProductComponent implements OnInit {
  @Input() product: Product;

  constructor() {}

  ngOnInit() {}

  onDeleteClick() {}
}
