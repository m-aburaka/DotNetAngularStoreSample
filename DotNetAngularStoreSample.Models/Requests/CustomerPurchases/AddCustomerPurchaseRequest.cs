using System;
using System.Collections.Generic;
using System.Text;
using DotNetAngularStoreSample.Models.Dtos;
using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.CustomerPurchases
{
    public class AddCustomerPurchaseRequest : IRequest<int>
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }

        public AddCustomerPurchaseRequest()
        {
            
        }

        public AddCustomerPurchaseRequest(int customerId, int productId)
        {
            CustomerId = customerId;
            ProductId = productId;
        }
    }
}
