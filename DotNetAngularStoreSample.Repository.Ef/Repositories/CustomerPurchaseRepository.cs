using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models;

namespace DotNetAngularStoreSample.Repository.Ef.Repositories
{
    public class CustomerPurchaseRepository : Repository<CustomerPurchase>, ICustomerPurchasesRepository
    {
        public CustomerPurchaseRepository(AppDbContext context) : base(context)
        {
        }
    }
}