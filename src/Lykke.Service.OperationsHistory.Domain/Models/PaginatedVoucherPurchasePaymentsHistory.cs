using System.Collections.Generic;

namespace Lykke.Service.OperationsHistory.Domain.Models
{
    public class PaginatedVoucherPurchasePaymentsHistory : BasePagedModel
    {
        public IEnumerable<IVoucherPurchasePayment> VoucherPurchasePaymentsHistory { get; set; }
    }
}
