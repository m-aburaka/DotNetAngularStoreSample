using DotNetAngularStoreSample.Models.Dtos;
using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.Customers
{
    public class GetCustomerPageRequest : IRequest<PagedResult<CustomerDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetCustomerPageRequest()
        {
            
        }

        public GetCustomerPageRequest(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
