using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using DotNetAngularStoreSample.Models.Dtos;
using DotNetAngularStoreSample.Models.Exceptions;
using DotNetAngularStoreSample.Models.Requests.Customers;
using DotNetAngularStoreSample.Server.Controllers;
using Xunit;

namespace DotNetAngularStoreSample.Server.Tests.Controllers
{
    public class CustomersControllerTests : BaseTestClass
    {
        private CustomersController Controller => new CustomersController(Mediator);

        [Fact]
        public async Task CreateAndGetAll_ShouldInsertAndGetAll()
        {
            var requests = AutoFixture.CreateMany<CreateCustomerRequest>().ToList();

            foreach (var r in requests)
                await Controller.Create(r);

            var customers = (await Controller.GetAll()).ToList();

            Assert.All(requests, r => 
                Assert.Contains(customers, dto => dto.Name == r.Name));
        }

        [Fact]
        public async Task CreateOneAndGetById_ShouldInsert()
        {
            var name = "Bruce";
            var request = new CreateCustomerRequest(name);
            var id = await Controller.Create(request);

            var customerById = await Controller.Get(id);
            Assert.Equal(name, customerById.Name);
        }

        [Fact]
        public async Task Get_NotExists_ShouldThrow()
        {
            var ex = await Assert.ThrowsAsync<NotFoundException>(async () => await Controller.Get(5555));
            Assert.Contains("not found", ex.Message.ToLower());
        }

        [Fact]
        public async Task GetPage()
        {
            var requests = AutoFixture.CreateMany<CreateCustomerRequest>(100).ToList();

            foreach (var r in requests)
                await Controller.Create(r);

            var customers = (await Controller.GetAll()).ToList();

            var pagesSize = 20;
            var pages = Math.Ceiling((double)customers.Count / pagesSize);

            var pagedCustomers = new List<CustomerDto>();
            for (int i = 0; i < pages; i++)
            {
                var page = await Controller.GetPage(new GetCustomerPageRequest(i, pagesSize));
                pagedCustomers.AddRange(page.Result);
            }

            Assert.Equal(customers.Count, pagedCustomers.Count);
        }

        [Fact]
        public async Task Delete_Existing_ShouldDelete()
        {
            var request = new CreateCustomerRequest("Test");
            var id = await Controller.Create(request);

            await Controller.Delete(new DeleteCustomerRequest(id));

            var customers = await Controller.GetAll();

            Assert.DoesNotContain(customers, c => c.Id == id);
        }

        [Fact]
        public async Task Delete_NotExisting_DoNothing()
        {
            await Controller.Delete(new DeleteCustomerRequest(55555));
        }
    }
}
