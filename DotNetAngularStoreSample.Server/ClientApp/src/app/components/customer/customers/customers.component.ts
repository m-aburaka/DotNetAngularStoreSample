import {
  Component,
  OnInit,
  ViewChild,
  ElementRef,
} from "@angular/core";
import { Customer } from "src/app/core/models/Customer";
import { CustomersService } from "src/app/core/services/customers.service";

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
  constructor(private customersService: CustomersService) {}

  ngOnInit() {
    this.update();
  }

  update() {
    this.customersService.getAll().subscribe(
      customers => {
        if (!customers) {
          return;
        }
        const empty = this.customers && this.customers.length === 0;
        this.customers = customers;
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
