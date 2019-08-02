using System.Linq;
using System.Threading.Tasks;
using DotNetAngularStoreSample.Models.Exceptions;
using DotNetAngularStoreSample.Models.Requests.CustomerPurchases;
using DotNetAngularStoreSample.Models.Requests.Customers;
using DotNetAngularStoreSample.Models.Requests.Products;
using DotNetAngularStoreSample.Server.Controllers;
using Xunit;

namespace DotNetAngularStoreSample.Server.Tests.Controllers
{
    public class CustomerPurchasesControllerTests : BaseTestClass
    {
        private CustomerPurchasesController PurchasesController => new CustomerPurchasesController(Mediator);
        private CustomersController CustomersController => new CustomersController(Mediator);
        private ProductsController ProductsController => new ProductsController(Mediator);

        [Fact]
        public async Task AddPurchases_ShouldAddForCustomer()
        {
            var customerRequest = new CreateCustomerRequest("George");
            var firstProductRequest = new CreateProductRequest("Tea");
            var secondProductRequest = new CreateProductRequest("Coffee");

            var customerId = await CustomersController.Create(customerRequest);
            var firstProductId = await ProductsController.Create(firstProductRequest);
            var secondProductId = await ProductsController.Create(secondProductRequest);

            var firstPurchaseRequest = new AddCustomerPurchaseRequest(customerId, firstProductId);
            var secondPurchaseRequest = new AddCustomerPurchaseRequest(customerId, secondProductId);
            
            await PurchasesController.Add(firstPurchaseRequest);
            await PurchasesController.Add(secondPurchaseRequest);

            var purchases = (await PurchasesController.Get(customerId)).ToList();
            
            Assert.Equal(2, purchases.Count);

            Assert.Contains(purchases, dto => dto.ProductName == "Tea");
            Assert.Contains(purchases, dto => dto.ProductName == "Coffee");
        }

        [Fact]
        public async Task AddPurchases_NonExistingCustomer_ShouldThrow()
        {
            var productRequest = new CreateProductRequest("Tea");
            var productId = await ProductsController.Create(productRequest);

            var request = new AddCustomerPurchaseRequest(555, productId);

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () =>
                await PurchasesController.Add(request));

            Assert.Contains("not found", ex.Message.ToLower());
            Assert.Contains("customer", ex.Message.ToLower());
        }

        [Fact]
        public async Task AddPurchases_NonExistingProduct_ShouldThrow()
        {
            var customerRequest = new CreateCustomerRequest("George");
            var customerId = await CustomersController.Create(customerRequest);

            var request = new AddCustomerPurchaseRequest(customerId, 555);

            var ex = await Assert.ThrowsAsync<NotFoundException>(async () =>
                await PurchasesController.Add(request));

            Assert.Contains("not found", ex.Message.ToLower());
            Assert.Contains("product", ex.Message.ToLower());
        }
        
        [Fact]
        public async Task Delete_Existing_ShouldDelete()
        {
            var customerRequest = new CreateCustomerRequest("George");
            var productRequest = new CreateProductRequest("Tea");

            var customerId = await CustomersController.Create(customerRequest);
            var productId = await ProductsController.Create(productRequest);

            var purchaseRequest = new AddCustomerPurchaseRequest(customerId, productId);
            
            var purchaseId = await PurchasesController.Add(purchaseRequest);

            await PurchasesController.Delete(new DeleteCustomerPurchaseRequest(purchaseId));

            var purchases = await PurchasesController.Get(customerId);

            Assert.DoesNotContain(purchases, c => c.Id == purchaseId);
        }

        [Fact]
        public async Task Delete_NotExisting_DoNothing()
        {
            await PurchasesController.Delete(new DeleteCustomerPurchaseRequest(5555));
        }
    }
}