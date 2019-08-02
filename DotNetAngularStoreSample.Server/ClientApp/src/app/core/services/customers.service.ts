import { Injectable, Inject } from "@angular/core";
import { Observable, Subject } from "rxjs";
import { Customer } from "../models/Customer";
import { HttpClient } from "@angular/common/http";
import { CreateCustomerRequest } from "./../models/CreateCustomerRequest";
import httpOptions from "./httpOptions";
import { DeleteCustomerRequest } from "../models/DeleteCustomerRequest";
import { PageResult } from "../models/PageResult";
import { GetPageRequest } from "./../models/GetPageRequest";

@Injectable({
  providedIn: "root"
})
export class CustomersService {
  private _customers: Customer[];

  private _customersSubject = new Subject<Customer[]>();

  private _count = new Subject<number>();

  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  getCount(): Observable<number> {
    return this._count.asObservable();
  }

  getPage(): Observable<Customer[]> {
    return this._customersSubject.asObservable();
  }

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

  fetchPage(page: number, pageSize: number) {
    const request = new GetPageRequest(page, pageSize);
    this.http
      .post<PageResult<Customer>>(
        this.baseUrl + "api/Customers/GetPage",
        request,
        httpOptions
      )
      .subscribe(
        response => {
          this._customers = response.result;
          this._count.next(response.itemsCount);
          console.log(this._customers);
          this._customersSubject.next(this._customers);
        },
        error => {
          this._customersSubject.error(error);
        }
      );
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
    const request = new DeleteCustomerRequest();
    request.customerId = customer.id;

    this.http
      .post(this.baseUrl + "api/Customers/Delete", request, httpOptions)
      .subscribe(
        _ => {
          this._customers = this._customers.filter(c => c.id !== customer.id);
          this._customersSubject.next(this._customers);
        },
        error => {
          this._customersSubject.error(error);
        }
      );
  }
}
