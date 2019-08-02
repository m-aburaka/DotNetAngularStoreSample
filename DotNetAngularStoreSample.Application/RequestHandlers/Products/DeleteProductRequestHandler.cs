using System.Threading;
using System.Threading.Tasks;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.Requests.Products;
using MediatR;

namespace DotNetAngularStoreSample.Application.RequestHandlers.Products
{
    public class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest>
    {
        private readonly IProductsRepository _productsRepository;

        public DeleteProductRequestHandler(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
        {
            await _productsRepository.Delete(request.ProductId);
            return Unit.Value;
        }
    }
}