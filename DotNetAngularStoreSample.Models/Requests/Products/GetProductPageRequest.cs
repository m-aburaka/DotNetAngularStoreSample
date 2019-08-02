using DotNetAngularStoreSample.Models.Dtos;
using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.Products
{
    public class GetProductPageRequest : IRequest<PagedResult<ProductDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetProductPageRequest()
        {

        }

        public GetProductPageRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}