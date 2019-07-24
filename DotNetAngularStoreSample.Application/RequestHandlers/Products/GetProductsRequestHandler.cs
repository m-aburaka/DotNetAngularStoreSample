using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.Dtos;
using DotNetAngularStoreSample.Models.Requests.Products;
using MediatR;

namespace DotNetAngularStoreSample.Application.RequestHandlers.Products
{
    public class GetProductsRequestHandler : IRequestHandler<GetProductsRequest, IEnumerable<ProductDto>>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public GetProductsRequestHandler(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var products = await _productsRepository.Get();

            var dto = _mapper.Map<ProductDto[]>(products);

            return dto;
        }
    }
}
