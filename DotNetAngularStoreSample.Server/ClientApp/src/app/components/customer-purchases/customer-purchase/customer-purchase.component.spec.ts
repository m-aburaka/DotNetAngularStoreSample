import { async, ComponentFixture, TestBed } from "@angular/core/testing";

import { CustomerPurcaseComponent } from "./customer-purchase.component";

describe("CustomerPurcaseComponent", () => {
  let component: CustomerPurcaseComponent;
  let fixture: ComponentFixture<CustomerPurcaseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [CustomerPurcaseComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerPurcaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
