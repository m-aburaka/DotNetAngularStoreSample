using System;
using System.Text;
using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models;

namespace DotNetAngularStoreSample.Repository.Ef.Repositories
{
    public class CustomersRepository : Repository<Customer>, ICustomersRepository
    {
        public CustomersRepository(AppDbContext context) : base(context)
        {
        }
    }
}
