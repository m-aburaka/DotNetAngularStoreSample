using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.Products
{
    public class CreateProductRequest : IRequest<int>
    {
        public string Name { get; }

        public CreateProductRequest(string name)
        {
            Name = name;
        }
    }
}
