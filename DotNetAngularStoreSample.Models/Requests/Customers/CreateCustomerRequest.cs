using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.Customers
{
    public class CreateCustomerRequest : IRequest<int>
    {
        public string Name { get; set; }

        public CreateCustomerRequest()
        {
            
        }

        public CreateCustomerRequest(string name)
        {
            Name = name;
        }
    }
}
