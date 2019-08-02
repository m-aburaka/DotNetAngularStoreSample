using System;
using System.Collections.Generic;
using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.Products
{
    public class DeleteProductRequest : IRequest
    {
        public int ProductId { get; set; }

        public DeleteProductRequest()
        {

        }

        public DeleteProductRequest(int productId)
        {
            ProductId = productId;
        }
    }
}