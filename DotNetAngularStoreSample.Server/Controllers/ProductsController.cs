using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetAngularStoreSample.Models.Dtos;
using DotNetAngularStoreSample.Models.Requests.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAngularStoreSample.Server.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return await _mediator.Send(new GetProductsRequest());
        }

        [HttpGet]
        public async Task<ProductDto> Get(int id)
        {
            return await _mediator.Send(new GetProductRequest(id));
        }

        [HttpPost]
        public async Task<int> Create(CreateProductRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}