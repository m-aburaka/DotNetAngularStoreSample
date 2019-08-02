using System.Threading;
using System.Threading.Tasks;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.Requests.Customers;
using MediatR;

namespace DotNetAngularStoreSample.Application.RequestHandlers.Customers
{
    public class DeleteCustomerRequestHandler : IRequestHandler<DeleteCustomerRequest>
    {
        private readonly ICustomersRepository _customersRepository;

        public DeleteCustomerRequestHandler(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }


        public async Task<Unit> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            await _customersRepository.Delete(request.CustomerId);
            return Unit.Value;
        }
    }
}