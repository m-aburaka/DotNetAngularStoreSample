using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetAngularStoreSample.Models.Dtos;
using DotNetAngularStoreSample.Models.Requests.Customers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAngularStoreSample.Server.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
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

        [HttpPost]
        public async Task<int> Create(CreateCustomerRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}
