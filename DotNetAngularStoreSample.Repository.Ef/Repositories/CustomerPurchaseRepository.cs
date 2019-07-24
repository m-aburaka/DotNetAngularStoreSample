using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DotNetAngularStoreSample.Repository.Ef.Repositories
{
    public class CustomerPurchaseRepository : Repository<CustomerPurchase>, ICustomerPurchasesRepository
    {
        public CustomerPurchaseRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CustomerPurchase>> GetForCustomer(int customerId)
        {
            var q = from p in Context.CustomerPurchases
                where p.CustomerId == customerId
                select p;

            return await q.ToListAsync();
        }
    }
}