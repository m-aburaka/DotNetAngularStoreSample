import { Injectable, Inject } from "@angular/core";
import { Observable, of } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { CustomerPurchase } from "../models/CustomerPurchase";

@Injectable({
  providedIn: "root"
})
export class CustomerPurchasesService {
  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  get(customerId: number): Observable<CustomerPurchase[]> {
    return this.http.get<CustomerPurchase[]>(
      this.baseUrl + `api/CustomerPurchases?customerId=${customerId}`
    );
  }
}
