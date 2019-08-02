using AutoMapper;
using DotNetAngularStoreSample.Models.DomainModels;
using DotNetAngularStoreSample.Models.Dtos;
using DotNetAngularStoreSample.Models.Requests.Customers;
using DotNetAngularStoreSample.Models.Requests.Products;

namespace DotNetAngularStoreSample.Application.IoC
{
    /// <summary>
    /// Creates mapper for data transfer objects
    /// </summary>
    public static class MapperFactory
    {
        public static IMapper Get()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDto>();
                cfg.CreateMap<CreateCustomerRequest, Customer>();
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<CreateProductRequest, Product>();
                cfg.CreateMap<CustomerPurchase, CustomerPurchaseDto>()
                    .ForMember(dto => dto.ProductName, e => e.MapFrom(purchase => purchase.Product.Name))
                        .ForMember(dto => dto.CustomerId, e => e.MapFrom(purchase => purchase.CustomerId));
            });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}