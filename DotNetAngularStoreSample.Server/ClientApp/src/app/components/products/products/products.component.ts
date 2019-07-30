import {
  Component,
  OnInit,
  ViewChild,
  ElementRef
} from "@angular/core";
import { Product } from "../../../core/models/Product";
import { ProductsService } from "../../../core/services/products.service";

@Component({
  selector: "app-products",
  templateUrl: "./products.component.html",
  styleUrls: ["../../shared.styles.css"]
})
export class ProductsComponent implements OnInit {
  @ViewChild("list", { read: ElementRef }) private list: ElementRef;
  products: Product[];
  loading = true;
  error: string;

  constructor(private productsService: ProductsService) {}

  ngOnInit() {
    this.update();
  }

  update() {
    this.productsService.getAll().subscribe(
      products => {
        if (!products) {
          return;
        }
        const empty = this.products && this.products.length === 0;
        this.products = products;
        this.loading = false;

        if (!empty && (this.list && this.list.nativeElement)) {
          setTimeout(() => {
            this.list.nativeElement.scrollTop = this.list.nativeElement.scrollHeight;
          }, 200);
        }
      },
      error => (this.error = error.message)
    );
  }
}
