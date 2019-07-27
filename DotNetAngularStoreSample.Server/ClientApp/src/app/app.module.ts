import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { AppComponent } from "./app.component";
import { HomeComponent } from "./components/home/home.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MaterialModule } from "./shared/material.module";
import { CustomersComponent } from "./components/customer/customers/customers.component";
import { ProductsComponent } from "./components/products/products/products.component";
import { CustomerComponent } from "./components/customer/customer/customer.component";
import { ProductComponent } from "./components/products/product/product.component";
import { CustomerNewComponent } from "./components/customer/customer-new/customer-new.component";
import { ProductNewComponent } from "./components/products/product-new/product-new.component";
import { CustomerPurchasesComponent } from "./components/customer-purchases/customer-purchases/customer-purchases.component";
import { CustomerPurcaseComponent } from "./components/customer-purchases/customer-purchase/customer-purchase.component";
import { CustomerPurchaseNewComponent } from "./components/customer-purchases/customer-purchase-new/customer-purchase-new.component";
import { SharedModule } from "./shared/shared.module";
import { CoreModule } from "./core/core.module";
import { AppRoutingModule } from "./app-routing.module";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CustomersComponent,
    ProductsComponent,
    CustomerComponent,
    ProductComponent,
    CustomerNewComponent,
    ProductNewComponent,
    CustomerPurchasesComponent,
    CustomerPurcaseComponent,
    CustomerPurchaseNewComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    FormsModule,
    ReactiveFormsModule,
    CoreModule,
    SharedModule,
    MaterialModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}
