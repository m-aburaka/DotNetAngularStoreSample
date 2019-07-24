using DotNetAngularStoreSample.Models.Dtos;
using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.Customers
{
    public class GetCustomerRequest : IRequest<CustomerDto>
    {
        public int Id { get; }
        public GetCustomerRequest(int id)
        {
            Id = id;
        }
    }
}