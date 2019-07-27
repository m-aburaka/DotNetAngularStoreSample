import { Component, OnInit, Input } from "@angular/core";
import { Router } from "@angular/router";
import { Customer } from "src/app/core/models/Customer";

@Component({
  selector: "app-customer",
  templateUrl: "./customer.component.html",
  styleUrls: ["../../shared.styles.css"]
})
export class CustomerComponent implements OnInit {
  @Input() customer: Customer;

  constructor(private router: Router) {}

  ngOnInit() {}

  onDeleteClick() {}
}
