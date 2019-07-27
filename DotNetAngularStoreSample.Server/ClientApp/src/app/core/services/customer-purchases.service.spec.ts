import { TestBed } from '@angular/core/testing';

import { CustomerPurchasesService } from './customer-purchases.service';

describe('CustomerPurchasesService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CustomerPurchasesService = TestBed.get(CustomerPurchasesService);
    expect(service).toBeTruthy();
  });
});
