import { Injectable, Inject } from "@angular/core";
import { Observable, of, BehaviorSubject } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { CustomerPurchase } from "../models/CustomerPurchase";
import httpOptions from "./httpOptions";
import AddCustomerPurchaseRequest from "../models/AddCustomerPurchaseRequest";
import { Customer } from "../models/Customer";
import { Product } from "../models/Product";
import { DeleteCustomerPurchaseRequest } from "./../models/DeleteCustomerPurhaseRequest";

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

  delete(purchase: CustomerPurchase) {
    const subject = this.getOrCreateSubject(purchase.customerId);
    const purchases = this.getOrCreatePurchases(purchase.customerId);

    const request = new DeleteCustomerPurchaseRequest();
    request.purchaseId = purchase.id;

    this.http
      .post(this.baseUrl + "api/CustomerPurchases/Delete", request, httpOptions)
      .subscribe(
        _ => {
          const newPurchases = purchases.filter(c => c.id !== purchase.id);
          this._purchasesByCustomer.set(purchase.customerId, newPurchases);
          subject.next(newPurchases);
        },
        error => {
          subject.error(error);
        }
      );
  }

  getOrCreateSubject(customerId: number) {
    let subject = this._customerPurchasesSubjectsByCustomer.get(customerId);
    if (!subject) {
      this._customerPurchasesSubjectsByCustomer.set(
        customerId,
        new BehaviorSubject<CustomerPurchase[]>(null)
      );
      subject = this._customerPurchasesSubjectsByCustomer.get(customerId);
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
