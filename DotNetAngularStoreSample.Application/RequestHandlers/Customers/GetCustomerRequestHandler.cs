using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.DomainModels;
using DotNetAngularStoreSample.Models.Dtos;
using DotNetAngularStoreSample.Models.Exceptions;
using DotNetAngularStoreSample.Models.Requests.Customers;
using MediatR;

namespace DotNetAngularStoreSample.Application.RequestHandlers.Customers
{
    public class GetCustomerRequestHandler : IRequestHandler<GetCustomerRequest, CustomerDto>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;

        public GetCustomerRequestHandler(ICustomersRepository customersRepository, IMapper mapper)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
        }
        public async Task<CustomerDto> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = await _customersRepository.Get(request.Id);

            if (customer == null)
                throw new NotFoundException($"{nameof(Customer)} not found by id {request.Id}");

            var dto = _mapper.Map<CustomerDto>(customer);

            return dto;
        }
    }
}