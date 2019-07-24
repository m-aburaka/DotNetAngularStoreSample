using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.DomainModels;
using DotNetAngularStoreSample.Models.Requests.Products;
using MediatR;

namespace DotNetAngularStoreSample.Application.RequestHandlers.Products
{
    public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, int>
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public CreateProductRequestHandler(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);

            await _productsRepository.Insert(product);

            return product.Id;
        }
    }
}