using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetAngularStoreSample.Models;
using DotNetAngularStoreSample.Models.DomainModels;

namespace DotNetAngularStoreSample.Application.Repositories
{
    public interface ICustomerPurchasesRepository : IRepository<CustomerPurchase>
    {
        Task<IEnumerable<CustomerPurchase>> GetForCustomer(int customerId);
    }
}