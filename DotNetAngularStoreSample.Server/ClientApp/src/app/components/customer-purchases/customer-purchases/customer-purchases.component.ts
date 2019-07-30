import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { CustomersService } from "../../../core/services/customers.service";
import { CustomerPurchase } from "../../../core/models/CustomerPurchase";
import { CustomerPurchasesService } from "../../../core/services/customer-purchases.service";
import { Customer } from "../../../core/models/Customer";

@Component({
  selector: "app-customer-purchases",
  templateUrl: "./customer-purchases.component.html",
  styleUrls: ["./customer-purchases.component.css", "../../shared.styles.css"]
})
export class CustomerPurchasesComponent implements OnInit {
  customer: Customer;
  purchases: CustomerPurchase[];
  customerLoading = true;
  purchasesLoading = true;

  public get loading(): boolean {
    return this.customerLoading || this.purchasesLoading;
  }

  error: string;

  constructor(
    private route: ActivatedRoute,
    private customersService: CustomersService,
    private customerPurchasesService: CustomerPurchasesService
  ) {}

  ngOnInit() {
    this.update();
  }

  update(): void {
    const id = +this.route.snapshot.paramMap.get("id");
    this.customersService.get(id).subscribe(
      customer => {
        this.customer = customer;
        this.customerLoading = false;
      },
      error => (this.error = error.message)
    );

    this.customerPurchasesService.get(id).subscribe(
      purchases => {
        this.purchases = purchases;
        this.purchasesLoading = false;
      },
      error => (this.error = error.message)
    );
  }
}
