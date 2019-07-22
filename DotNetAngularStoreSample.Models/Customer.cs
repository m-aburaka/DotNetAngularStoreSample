using System;
using System.Collections;
using System.Collections.Generic;

namespace DotNetAngularStoreSample.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<CustomerPurchase> CustomerPurchases { get; set; } = new List<CustomerPurchase>();
    }
}
