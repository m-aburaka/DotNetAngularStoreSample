using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetAngularStoreSample.Models.Dtos;
using DotNetAngularStoreSample.Models.Requests.CustomerPurchases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAngularStoreSample.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPurchasesController : Controller
    {
        private readonly IMediator _mediator;

        public CustomerPurchasesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IEnumerable<CustomerPurchaseDto>> Get(int customerId)
        {
            return await _mediator.Send(new GetCustomerPurchasesRequest(customerId));
        }

        [HttpPost]
        public async Task<int> Add(AddCustomerPurchaseRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}