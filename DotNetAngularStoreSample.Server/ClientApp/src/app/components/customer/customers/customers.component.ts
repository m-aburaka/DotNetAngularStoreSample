import { Component, OnInit, ViewChild, ElementRef } from "@angular/core";
import { Customer } from "src/app/core/models/Customer";
import { CustomersService } from "src/app/core/services/customers.service";
import { PageEvent } from "@angular/material/paginator";

@Component({
  selector: "app-customers",
  templateUrl: "./customers.component.html",
  styleUrls: ["../../shared.styles.css"]
})
export class CustomersComponent implements OnInit {
  @ViewChild("list", { read: ElementRef }) private list: ElementRef;
  customers: Customer[];
  loading = true;
  error: string;

  // MatPaginator Inputs
  itemsCount = 100;
  pageSize = 5;
  pageSizeOptions: number[] = [5, 10, 25, 100];

  pageIndex = 0;

  constructor(private customersService: CustomersService) {}

  ngOnInit() {
    this.customersService.getPage().subscribe(
      customers => {
        const wasEmpty = this.customers && this.customers.length === 0;

        this.customers = customers;
        this.loading = false;

        if (!wasEmpty && (this.list && this.list.nativeElement)) {
          setTimeout(() => {
            this.list.nativeElement.scrollTop = this.list.nativeElement.scrollHeight;
          }, 200);
        }
      },
      error => (this.error = error.message)
    );

    this.customersService.getCount().subscribe(count => {
      this.itemsCount = count;
    });
    this.onPaginationChange(0, 5);
  }

  onPaginationChange(pageIndex: number, pageSize: number) {
    this.loading = true;
    this.pageIndex = pageIndex;
    this.pageSize = pageSize;
    this.customersService.fetchPage(this.pageIndex, this.pageSize);
  }
}
