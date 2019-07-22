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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = 1,
                Name = "Bob",
            }, new Customer
            {
                Id = 2,
                Name = "Alice",
            }, new Customer
            {
                Id = 3,
                Name = "John",
            }, new Customer
            {
                Id = 4,
                Name = "Bill",
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 1,
                Name = "Cola",
            }, new Product
            {
                Id = 2,
                Name = "Pepsi",
            }, new Product
            {
                Id = 3,
                Name = "Bacardi",
            }, new Product
            {
                Id = 4,
                Name = "Absolut",
            });


            modelBuilder.Entity<CustomerPurchase>().HasData(new CustomerPurchase
            {
                Id = 1,
                CustomerId = 1,
                ProductId = 1,
            }, new CustomerPurchase
            {
                Id = 2,
                CustomerId = 2,
                ProductId = 2,
            }, new CustomerPurchase
            {
                Id = 3,
                CustomerId = 3,
                ProductId = 3,
            }, new CustomerPurchase
            {
                Id = 4,
                CustomerId = 4,
                ProductId = 4,
            });
        }
    }
}
