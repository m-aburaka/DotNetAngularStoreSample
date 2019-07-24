using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.Dtos;
using DotNetAngularStoreSample.Models.Requests.Customers;
using MediatR;

namespace DotNetAngularStoreSample.Application.RequestHandlers.Customers
{
    public class GetCustomersRequestHandler : IRequestHandler<GetCustomersRequest, IEnumerable<CustomerDto>>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;

        public GetCustomersRequestHandler(ICustomersRepository customersRepository, IMapper mapper)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CustomerDto>> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        {
            var customers = await _customersRepository.Get();

            var dto = _mapper.Map<CustomerDto[]>(customers);

            return dto;
        }
    }
}
