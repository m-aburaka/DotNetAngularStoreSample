import { Injectable, Inject } from "@angular/core";
import { Observable, of } from "rxjs";
import { Product } from "../models/Product";
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: "root"
})
export class ProductsService {
  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  get(): Observable<Product[]> {
    return this.http.get<Product[]>(this.baseUrl + "api/Products/GetAll");
  }
}
