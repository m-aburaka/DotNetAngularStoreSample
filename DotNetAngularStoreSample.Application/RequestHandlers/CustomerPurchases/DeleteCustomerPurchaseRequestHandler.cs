using System.Threading;
using System.Threading.Tasks;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.Requests.CustomerPurchases;
using MediatR;

namespace DotNetAngularStoreSample.Application.RequestHandlers.CustomerPurchases
{
    public class DeleteCustomerPurchaseRequestHandler : IRequestHandler<DeleteCustomerPurchaseRequest>
    {
        private readonly ICustomerPurchasesRepository _customerPurchasesRepository;

        public DeleteCustomerPurchaseRequestHandler(ICustomerPurchasesRepository customerPurchasesRepository)
        {
            _customerPurchasesRepository = customerPurchasesRepository;
        }

        public async Task<Unit> Handle(DeleteCustomerPurchaseRequest request, CancellationToken cancellationToken)
        {
            await _customerPurchasesRepository.Delete(request.PurchaseId);
            return Unit.Value;
        }
    }
}