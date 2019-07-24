using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.DomainModels;
using DotNetAngularStoreSample.Models.Dtos;
using DotNetAngularStoreSample.Models.Exceptions;
using DotNetAngularStoreSample.Models.Requests.Products;
using MediatR;

namespace DotNetAngularStoreSample.Application.RequestHandlers.Products
{
    public class GetProductRequestHandler : IRequestHandler<GetProductRequest, ProductDto>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public GetProductRequestHandler(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {
            var product = await _productsRepository.Get(request.Id);

            if (product == null)
                throw new NotFoundException($"{nameof(Product)} not found by id {request.Id}");

            var dto = _mapper.Map<ProductDto>(product);

            return dto;
        }
    }
}