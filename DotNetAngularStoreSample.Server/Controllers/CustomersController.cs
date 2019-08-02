using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetAngularStoreSample.Models.Dtos;
using DotNetAngularStoreSample.Models.Exceptions;
using DotNetAngularStoreSample.Models.Requests.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAngularStoreSample.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<CustomerDto>> GetAll()
        {
            return await _mediator.Send(new GetCustomersRequest());
        }

        [HttpGet]
        public async Task<CustomerDto> Get(int id)
        {
            return await _mediator.Send(new GetCustomerRequest(id));
        }

        [HttpPost("[action]")]
        public async Task<PagedResult<CustomerDto>> GetPage(GetCustomerPageRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<int> Create(CreateCustomerRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpPost("[action]")]
        public async Task Delete(DeleteCustomerRequest request)
        {
            await _mediator.Send(request);
        }
    }
}
