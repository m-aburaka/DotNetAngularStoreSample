using System.Collections.Generic;
using DotNetAngularStoreSample.Models.Dtos;
using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.Customers
{
    public class GetCustomersRequest : IRequest<IEnumerable<CustomerDto>>
    {

    }
}