import { Component, OnInit } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import { ProductsService } from "src/app/core/services/products.service";

@Component({
  selector: "app-product-new",
  templateUrl: "./product-new.component.html",
  styleUrls: ["../../shared.styles.css"]
})
export class ProductNewComponent implements OnInit {
  nameControl = new FormControl("", [Validators.required]);
  constructor(private productsService: ProductsService) {}

  ngOnInit() {}

  onEnterKeyDown(name: string) {
    this.onAddClick(name);
  }

  onAddClick(name: string) {
    this.nameControl.markAsTouched();
    if (name) {
      this.productsService.create(name);
      this.nameControl.reset();
    }
  }
}
