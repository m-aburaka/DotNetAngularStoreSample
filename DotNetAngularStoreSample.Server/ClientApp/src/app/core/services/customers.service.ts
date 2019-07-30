import { Injectable, Inject } from "@angular/core";
import { Observable, of, BehaviorSubject } from "rxjs";
import { Customer } from "../models/Customer";
import { HttpClient } from "@angular/common/http";
import { CreateCustomerRequest } from "./../models/CreateCustomerRequest";
import httpOptions from "./httpOptions";

@Injectable({
  providedIn: "root"
})
export class CustomersService {
  private _customers: Customer[];

  private _customersSubject = new BehaviorSubject<Customer[]>(null);

  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  getAll(): Observable<Customer[]> {
    this.http.get<Customer[]>(this.baseUrl + "api/Customers/GetAll").subscribe(
      customers => {
        this._customers = customers;
        this._customersSubject.next(customers);
      },
      error => {
        this._customersSubject.error(error);
      }
    );
    return this._customersSubject.asObservable();
  }

  get(id: number): Observable<Customer> {
    return this.http.get<Customer>(this.baseUrl + `api/Customers?id=${id}`);
  }

  create(name: string) {
    const request = new CreateCustomerRequest();
    request.name = name;
    this.http
      .post<number>(this.baseUrl + `api/Customers`, request, httpOptions)
      .subscribe(
        id => {
          const customer = new Customer();
          customer.name = name;
          customer.id = id;
          this._customers.push(customer);
          this._customersSubject.next(this._customers);
        },
        error => {
          this._customersSubject.error(error);
        }
      );
  }

  delete(customer: Customer) {
    //this.http
    //  .post(this.baseUrl + "api/Customers/Delete", request, httpOptions)
    //  .subscribe({});
  }
}
