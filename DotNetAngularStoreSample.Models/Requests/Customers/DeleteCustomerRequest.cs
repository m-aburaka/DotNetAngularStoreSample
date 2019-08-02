using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.Customers
{
    public class DeleteCustomerRequest : IRequest
    {
        public int CustomerId { get; set; }

        public DeleteCustomerRequest()
        {
            
        }

        public DeleteCustomerRequest(int customerId)
        {
            CustomerId = customerId;
        }

    }
}