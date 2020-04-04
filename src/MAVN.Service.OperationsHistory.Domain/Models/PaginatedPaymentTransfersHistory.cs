using System.Collections.Generic;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class PaginatedPaymentTransfersHistory : BasePagedModel
    {
        public IEnumerable<IPaymentTransfer> PaymentTransfersHistory { get; set; }
    }
}
