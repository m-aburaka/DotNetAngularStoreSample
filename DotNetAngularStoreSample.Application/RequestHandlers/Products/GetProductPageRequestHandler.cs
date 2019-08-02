using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.Dtos;
using DotNetAngularStoreSample.Models.Requests.Customers;
using DotNetAngularStoreSample.Models.Requests.Products;
using MediatR;

namespace DotNetAngularStoreSample.Application.RequestHandlers.Products
{
    public class GetProductPageRequestHandler : IRequestHandler<GetProductPageRequest, PagedResult<ProductDto>>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public GetProductPageRequestHandler(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ProductDto>> Handle(GetProductPageRequest request, CancellationToken cancellationToken)
        {
            var customers = await _productsRepository.GetPage(request.PageNumber, request.PageSize);
            var dtos = _mapper.Map<ProductDto[]>(customers);

            var result = new PagedResult<ProductDto>();
            result.PageNumber = request.PageNumber;
            result.Result = dtos;
            result.PageSize = request.PageSize;
            result.ItemsCount = await _productsRepository.Count();
            result.PageCount = (int)Math.Ceiling((double)result.ItemsCount / result.PageSize);


            return result;
        }
    }
}
