using System.Collections.Generic;
using DotNetAngularStoreSample.Models.Dtos;
using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.CustomerPurchases
{
    public class GetCustomerPurchasesRequest : IRequest<IEnumerable<CustomerPurchaseDto>>
    {
        public int CustomerId { get; }

        public GetCustomerPurchasesRequest(int customerId)
        {
            CustomerId = customerId;
        }
    }
}