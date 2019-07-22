using DotNetAngularStoreSample.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetAngularStoreSample.Repository.Ef
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerPurchase> CustomerPurchases { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
