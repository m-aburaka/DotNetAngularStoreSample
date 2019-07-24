using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using DotNetAngularStoreSample.Models.Exceptions;
using DotNetAngularStoreSample.Models.Requests.Products;
using DotNetAngularStoreSample.Server.Controllers;
using Xunit;

namespace DotNetAngularStoreSample.Server.Tests.Controllers
{
    public class ProductsControllerTests : BaseTestClass
    {
        private ProductsController Controller => new ProductsController(Mediator);

        [Fact]
        public async Task CreateAndGetAll_ShouldInsertAndGetAll()
        {
            var requests = AutoFixture.CreateMany<CreateProductRequest>().ToList();

            foreach (var r in requests)
                await Controller.Create(r);

            var products = (await Controller.GetAll()).ToList();

            Assert.All(requests, r =>
                Assert.Contains(products, dto => dto.Name == r.Name));
        }

        [Fact]
        public async Task CreateOneAndGetById_ShouldInsert()
        {
            var name = "Mars";
            var request = new CreateProductRequest(name);
            var id = await Controller.Create(request);

            var productById = await Controller.Get(id);
            Assert.Equal(name, productById.Name);
        }

        [Fact]
        public async Task Get_NotExists_ShouldThrow()
        {
            var ex = await Assert.ThrowsAsync<NotFoundException>(async () => await Controller.Get(5555));
            Assert.Contains("not found", ex.Message.ToLower());
        }
    }
}