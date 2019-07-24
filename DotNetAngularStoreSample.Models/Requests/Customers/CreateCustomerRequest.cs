using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.Customers
{
    public class CreateCustomerRequest : IRequest<int>
    {
        public string Name { get; }

        public CreateCustomerRequest(string name)
        {
            Name = name;
        }
    }
}
