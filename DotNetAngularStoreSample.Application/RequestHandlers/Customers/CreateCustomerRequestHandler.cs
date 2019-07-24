using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.DomainModels;
using DotNetAngularStoreSample.Models.Requests.Customers;
using MediatR;

namespace DotNetAngularStoreSample.Application.RequestHandlers.Customers
{
    public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, int>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;

        public CreateCustomerRequestHandler(ICustomersRepository customersRepository, IMapper mapper)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);

            await _customersRepository.Insert(customer);

            return customer.Id;
        }
    }
}