using DotNetAngularStoreSample.Models.Dtos;
using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.Products
{
    public class GetProductRequest : IRequest<ProductDto>
    {
        public int Id { get; }
        public GetProductRequest(int id)
        {
            Id = id;
        }
    }
}