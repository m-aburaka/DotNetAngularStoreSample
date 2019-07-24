using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.DomainModels;

namespace DotNetAngularStoreSample.Repository.Ef.Repositories
{
    public class CustomersRepository : Repository<Customer>, ICustomersRepository
    {
        public CustomersRepository(AppDbContext context) : base(context)
        {
        }
    }
}
