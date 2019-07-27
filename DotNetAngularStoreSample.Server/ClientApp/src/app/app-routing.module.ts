import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HomeComponent } from "./components/home/home.component";
import { CustomerPurchasesComponent } from "./components/customer-purchases/customer-purchases/customer-purchases.component";
import { NotFoundComponent } from "./shared/components/not-found/not-found.component";

@NgModule({
  imports: [
    RouterModule.forRoot([
      { path: "", component: HomeComponent, pathMatch: "full" },
      { path: "customers/:id", component: CustomerPurchasesComponent },
      { path: "**", component: NotFoundComponent }
    ])
  ],

  exports: [RouterModule]
})
export class AppRoutingModule {}
