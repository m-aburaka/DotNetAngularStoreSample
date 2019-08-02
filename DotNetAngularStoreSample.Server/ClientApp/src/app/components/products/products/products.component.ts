import { Component, OnInit, ViewChild, ElementRef } from "@angular/core";
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

  // MatPaginator Inputs
  itemsCount = 100;
  pageSize = 5;
  pageSizeOptions: number[] = [5, 10, 25, 100];

  pageIndex = 0;

  constructor(private productsService: ProductsService) {}

  ngOnInit() {
    this.productsService.getPage().subscribe(
      products => {
        const wasEmpty = this.products && this.products.length === 0;

        this.products = products;
        this.loading = false;

        if (!wasEmpty && (this.list && this.list.nativeElement)) {
          setTimeout(() => {
            this.list.nativeElement.scrollTop = this.list.nativeElement.scrollHeight;
          }, 200);
        }
      },
      error => (this.error = error.message)
    );

    this.productsService.getCount().subscribe(count => {
      this.itemsCount = count;
    });
    this.onPaginationChange(0, 5);
  }

  onPaginationChange(pageIndex: number, pageSize: number) {
    this.loading = true;
    this.pageIndex = pageIndex;
    this.pageSize = pageSize;
    this.productsService.fetchPage(this.pageIndex, this.pageSize);
  }
}
