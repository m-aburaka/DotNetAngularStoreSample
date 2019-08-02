using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.Dtos;
using DotNetAngularStoreSample.Models.Requests.Customers;
using MediatR;

namespace DotNetAngularStoreSample.Application.RequestHandlers.Customers
{
    public class GetCustomerPageRequestHandler : IRequestHandler<GetCustomerPageRequest, PagedResult<CustomerDto>>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;

        public GetCustomerPageRequestHandler(ICustomersRepository customersRepository, IMapper mapper)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<CustomerDto>> Handle(GetCustomerPageRequest request, CancellationToken cancellationToken)
        {
            var customers = await _customersRepository.GetPage(request.PageNumber, request.PageSize);
            var dtos = _mapper.Map<CustomerDto[]>(customers);

            var result = new PagedResult<CustomerDto>();
            result.PageNumber = request.PageNumber;
            result.Result = dtos;
            result.PageSize = request.PageSize;
            result.ItemsCount = await _customersRepository.Count();
            result.PageCount = (int) Math.Ceiling((double) result.ItemsCount / result.PageSize);
            

            return result;
        }
    }
}
