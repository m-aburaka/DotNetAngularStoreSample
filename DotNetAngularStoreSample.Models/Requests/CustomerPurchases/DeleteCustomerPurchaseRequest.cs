using MediatR;

namespace DotNetAngularStoreSample.Models.Requests.CustomerPurchases
{
    public class DeleteCustomerPurchaseRequest : IRequest
    {
        public int PurchaseId { get; set; }

        public DeleteCustomerPurchaseRequest()
        {

        }

        public DeleteCustomerPurchaseRequest(int purchaseId)
        {
            PurchaseId = purchaseId;
        }
    }
}