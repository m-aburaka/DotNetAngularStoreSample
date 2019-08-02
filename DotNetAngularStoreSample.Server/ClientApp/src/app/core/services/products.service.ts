import { Injectable, Inject } from "@angular/core";
import { Observable, of, BehaviorSubject, Subject } from "rxjs";
import { Product } from "../models/Product";
import { HttpClient } from "@angular/common/http";
import CreateProductRequest from "../models/CreateProductRequest";
import httpOptions from "./httpOptions";
import { DeleteProductRequest } from "../models/DeleteProductRequest";
import { GetPageRequest } from "../models/GetPageRequest";
import { PageResult } from "../models/PageResult";

@Injectable({
  providedIn: "root"
})
export class ProductsService {
  private _products: Product[];
  private _productsSubject = new Subject<Product[]>();

  private _count = new Subject<number>();

  constructor(
    private http: HttpClient,
    @Inject("BASE_URL") private baseUrl: string
  ) {}

  getCount(): Observable<number> {
    return this._count.asObservable();
  }
  getPage(): Observable<Product[]> {
    return this._productsSubject.asObservable();
  }

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

  fetchPage(page: number, pageSize: number) {
    const request = new GetPageRequest(page, pageSize);
    this.http
      .post<PageResult<Product>>(
        this.baseUrl + "api/Products/GetPage",
        request,
        httpOptions
      )
      .subscribe(
        response => {
          this._products = response.result;
          this._count.next(response.itemsCount);
          console.log(this._products);
          this._productsSubject.next(this._products);
        },
        error => {
          this._productsSubject.error(error);
        }
      );
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

  delete(product: Product) {
    const request = new DeleteProductRequest();
    request.productId = product.id;

    this.http
      .post(this.baseUrl + "api/Products/Delete", request, httpOptions)
      .subscribe(
        _ => {
          this._products = this._products.filter(c => c.id !== product.id);
          this._productsSubject.next(this._products);
        },
        error => {
          this._productsSubject.error(error);
        }
      );
  }
}
