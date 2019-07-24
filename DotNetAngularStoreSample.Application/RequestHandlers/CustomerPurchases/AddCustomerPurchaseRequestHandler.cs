using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.DomainModels;
using DotNetAngularStoreSample.Models.Exceptions;
using DotNetAngularStoreSample.Models.Requests.CustomerPurchases;
using MediatR;

namespace DotNetAngularStoreSample.Application.RequestHandlers.CustomerPurchases
{
    public class AddCustomerPurchaseRequestHandler : IRequestHandler<AddCustomerPurchaseRequest, int>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ICustomerPurchasesRepository _customerPurchasesRepository;

        public AddCustomerPurchaseRequestHandler(
            ICustomersRepository customersRepository,
            IProductsRepository productsRepository,
            ICustomerPurchasesRepository customerPurchasesRepository)
        {
            _customersRepository = customersRepository;
            _productsRepository = productsRepository;
            _customerPurchasesRepository = customerPurchasesRepository;
        }

        public async Task<int> Handle(AddCustomerPurchaseRequest request, CancellationToken cancellationToken)
        {
            if (!await _customersRepository.Exists(request.CustomerId))
                throw new NotFoundException($"{nameof(Customer)} not found by id {request.CustomerId}");

            if (!await _productsRepository.Exists(request.ProductId))
                throw new NotFoundException($"{nameof(Product)} not found by id {request.ProductId}");

            var purchase = new CustomerPurchase
            {
                CustomerId = request.CustomerId,
                ProductId = request.ProductId
            };

            await _customerPurchasesRepository.Insert(purchase);

            return purchase.Id;
        }
    }
}
