import { Component, OnInit } from "@angular/core";
import { Customer } from "src/app/core/models/Customer";
import { CustomersService } from "src/app/core/services/customers.service";

@Component({
  selector: "app-customers",
  templateUrl: "./customers.component.html",
  styleUrls: ["../../shared.styles.css"]
})
export class CustomersComponent implements OnInit {
  customers: Customer[];
  loading = true;
  error: string;
  constructor(private customersService: CustomersService) {}

  ngOnInit() {
    this.update();
  }

  update() {
    this.customersService.getAll().subscribe(
      customers => {
        this.customers = customers;
        this.loading = false;
      },
      error => (this.error = error.message)
    );
  }

  onAddClick() {}
}
