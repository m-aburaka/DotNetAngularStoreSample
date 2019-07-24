using DotNetAngularStoreSample.Application.Repositories;
using DotNetAngularStoreSample.Models;
using DotNetAngularStoreSample.Models.DomainModels;

namespace DotNetAngularStoreSample.Repository.Ef.Repositories
{
    public class ProductsRepository : Repository<Product>, IProductsRepository
    {
        public ProductsRepository(AppDbContext context) : base(context)
        {
        }
    }
}