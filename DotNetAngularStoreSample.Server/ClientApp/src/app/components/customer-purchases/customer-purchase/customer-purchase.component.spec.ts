import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { CustomerPurchaseComponent } from "./customer-purchase.component";

describe("CustomerPurcaseComponent", () => {
  let component: CustomerPurchaseComponent;
  let fixture: ComponentFixture<CustomerPurchaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CustomerPurchaseComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerPurchaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
