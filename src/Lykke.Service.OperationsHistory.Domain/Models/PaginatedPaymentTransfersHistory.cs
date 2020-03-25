using System.Collections.Generic;

namespace Lykke.Service.OperationsHistory.Domain.Models
{
    public class PaginatedPaymentTransfersHistory : BasePagedModel
    {
        public IEnumerable<IPaymentTransfer> PaymentTransfersHistory { get; set; }
    }
}
