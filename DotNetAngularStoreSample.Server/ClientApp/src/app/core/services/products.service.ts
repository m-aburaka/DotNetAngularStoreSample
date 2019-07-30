import { Injectable, Inject } from "@angular/core";
import { Observable, of, BehaviorSubject } from "rxjs";
import { Product } from "../models/Product";
import { HttpClient } from "@angular/common/http";
import CreateProductRequest from "../models/CreateProductRequest";
import httpOptions from "./httpOptions";

@Injectable({
  providedIn: "root"
})
export class ProductsService {
  private _products: Product[];
  private _productsSubject = new BehaviorSubject<Product[]>(null);

  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  getAll(): Observable<Product[]> {
    this.http.get<Product[]>(this.baseUrl + "api/Products/GetAll").subscribe(
      products => {
        this._products = products;
        this._productsSubject.next(products);
      },
      error => {
        this._productsSubject.error(error);
      }
    );
    return this._productsSubject.asObservable();
  }

  create(name: string) {
    const request = new CreateProductRequest();
    request.name = name;
    this.http
      .post<number>(this.baseUrl + `api/Products`, request, httpOptions)
      .subscribe(
        id => {
          const product = new Product();
          product.name = name;
          product.id = id;
          this._products.push(product);
          this._productsSubject.next(this._products);
        },
        error => {
          this._productsSubject.error(error);
        }
      );
  }
}
