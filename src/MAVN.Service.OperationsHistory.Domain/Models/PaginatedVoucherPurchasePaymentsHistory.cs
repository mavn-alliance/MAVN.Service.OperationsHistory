using System.Collections.Generic;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class PaginatedVoucherPurchasePaymentsHistory : BasePagedModel
    {
        public IEnumerable<IVoucherPurchasePayment> VoucherPurchasePaymentsHistory { get; set; }
    }
}
