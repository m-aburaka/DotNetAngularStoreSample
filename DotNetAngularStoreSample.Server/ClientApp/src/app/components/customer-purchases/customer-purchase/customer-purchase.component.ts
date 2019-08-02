import { Component, OnInit, Input } from "@angular/core";
import { CustomerPurchase } from "../../../core/models/CustomerPurchase";
import { CustomerPurchasesService } from "./../../../core/services/customer-purchases.service";

@Component({
  selector: "app-customer-purchase",
  templateUrl: "./customer-purchase.component.html",
  styleUrls: ["../../shared.styles.css"]
})
export class CustomerPurchaseComponent implements OnInit {
  @Input() purchase: CustomerPurchase;

  constructor(private customerPurchasesService: CustomerPurchasesService) {}

  ngOnInit() {}

  onDeleteClick() {
    this.customerPurchasesService.delete(this.purchase);
  }
}
