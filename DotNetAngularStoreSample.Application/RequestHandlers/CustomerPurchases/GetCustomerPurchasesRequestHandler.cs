using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.Dtos;
using DotNetAngularStoreSample.Models.Requests.CustomerPurchases;
using MediatR;

namespace DotNetAngularStoreSample.Application.RequestHandlers.CustomerPurchases
{
    public class GetCustomerPurchasesRequestHandler : IRequestHandler<GetCustomerPurchasesRequest, IEnumerable<CustomerPurchaseDto>>
    {
        private readonly ICustomerPurchasesRepository _customerPurchasesRepository;
        private readonly IMapper _mapper;

        public GetCustomerPurchasesRequestHandler(ICustomerPurchasesRepository customerPurchasesRepository,
            IMapper mapper)
        {
            _customerPurchasesRepository = customerPurchasesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerPurchaseDto>> Handle(GetCustomerPurchasesRequest request, CancellationToken cancellationToken)
        {
            var purchases = await _customerPurchasesRepository.GetForCustomer(request.CustomerId);

            var dtos = _mapper.Map<CustomerPurchaseDto[]>(purchases);

            return dtos;
        }
    }
}