import { Component, OnInit } from "@angular/core";
import { CustomersService } from "src/app/core/services/customers.service";
import { FormControl, Validators } from "@angular/forms";

@Component({
  selector: "app-customer-new",
  templateUrl: "./customer-new.component.html",
  styleUrls: ["../../shared.styles.css"]
})
export class CustomerNewComponent implements OnInit {
  nameControl = new FormControl("", [Validators.required]);

  constructor(private customersService: CustomersService) {}

  ngOnInit() {}

  onEnterKeyDown(name: string) {
    this.onAddClick(name);
  }

  onAddClick(name: string) {
    this.nameControl.markAsTouched();
    if (name) {
      this.customersService.create(name);
      this.nameControl.reset();
    }
  }
}
