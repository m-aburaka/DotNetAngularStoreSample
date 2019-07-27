import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { CustomerPurchaseNewComponent } from "./customer-purchase-new.component";

describe("CustomerPurchaseNewComponent", () => {
  let component: CustomerPurchaseNewComponent;
  let fixture: ComponentFixture<CustomerPurchaseNewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CustomerPurchaseNewComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerPurchaseNewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
