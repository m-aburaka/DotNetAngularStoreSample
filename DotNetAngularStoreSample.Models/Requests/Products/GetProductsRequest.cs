using System.Collections.Generic;
using DotNetAngularStoreSample.Models.Dtos;
using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.Products
{
    public class GetProductsRequest : IRequest<IEnumerable<ProductDto>>
    {

    }
}