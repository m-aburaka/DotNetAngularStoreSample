import { Injectable, Inject } from "@angular/core";
import { Observable, of, BehaviorSubject } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { CustomerPurchase } from "../models/CustomerPurchase";
import httpOptions from "./httpOptions";
import AddCustomerPurchaseRequest from "../models/AddCustomerPurchaseRequest";
import { Customer } from "../models/Customer";
import { Product } from "../models/Product";

@Injectable({
  providedIn: "root"
})
export class CustomerPurchasesService {
  private _purchasesByCustomer = new Map<number, CustomerPurchase[]>();

  private _customerPurchasesSubjectsByCustomer = new Map<
    number,
    BehaviorSubject<CustomerPurchase[]>
  >();

  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  get(customerId: number): Observable<CustomerPurchase[]> {
    const subject = this.getOrCreateSubject(customerId);
    const purchases = this.getOrCreatePurchases(customerId);

    this.http
      .get<CustomerPurchase[]>(
        this.baseUrl + `api/CustomerPurchases?customerId=${customerId}`
      )
      .subscribe(
        newPurchases => {
          this._purchasesByCustomer.set(customerId, newPurchases);
          subject.next(newPurchases);
        },
        error => {
          subject.error(error);
        }
      );
    return subject.asObservable();
  }

  add(customer: Customer, product: Product) {
    const subject = this.getOrCreateSubject(customer.id);
    const purchases = this.getOrCreatePurchases(customer.id);

    const request = new AddCustomerPurchaseRequest();
    request.customerId = customer.id;
    request.productId = product.id;
    return this.http
      .post<number>(
        this.baseUrl + `api/CustomerPurchases`,
        request,
        httpOptions
      )
      .subscribe(
        id => {
          const purchase = new CustomerPurchase();
          purchase.productName = product.name;
          purchase.id = id;
          purchases.push(purchase);
          subject.next(purchases);
        },
        error => {
          subject.error(error);
        }
      );
  }

  getOrCreateSubject(customerId: number) {
    let subject = this._customerPurchasesSubjectsByCustomer.get(customerId);
    if (!subject) {
      console.log("creating subject");
      this._customerPurchasesSubjectsByCustomer.set(
        customerId,
        new BehaviorSubject<CustomerPurchase[]>(null)
      );
      subject = this._customerPurchasesSubjectsByCustomer.get(customerId);
      console.log("created - ", subject);
      console.log("subjects  - ", this._customerPurchasesSubjectsByCustomer);
    }
    return subject;
  }

  getOrCreatePurchases(customerId: number) {
    let purchases = this._purchasesByCustomer.get(customerId);
    if (!purchases) {
      this._purchasesByCustomer.set(customerId, []);
      purchases = this._purchasesByCustomer.get(customerId);
    }
    return purchases;
  }
}
