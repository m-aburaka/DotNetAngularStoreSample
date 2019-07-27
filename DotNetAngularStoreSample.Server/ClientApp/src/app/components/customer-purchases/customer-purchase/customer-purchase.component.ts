import { Component, OnInit, Input } from "@angular/core";
import { CustomerPurchase } from "../../../core/models/CustomerPurchase";

@Component({
  selector: "app-customer-purchase",
  templateUrl: "./customer-purchase.component.html",
  styleUrls: ["../../shared.styles.css"]
})
export class CustomerPurcaseComponent implements OnInit {
  @Input() purchase: CustomerPurchase;

  constructor() {}

  ngOnInit() {}

  onDeleteClick() {}
}
