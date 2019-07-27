import { Injectable, Inject } from "@angular/core";
import { Observable, of } from "rxjs";
import { Customer } from "../models/Customer";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class CustomersService {
  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  getAll(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.baseUrl + "api/Customers/GetAll");
  }

  get(id: number): Observable<Customer> {
    return this.http.get<Customer>(this.baseUrl + `api/Customers?id=${id}`);
  }
}
