using System.Collections.Generic;
using JetBrains.Annotations;

namespace Lykke.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Gives the List of voucher purchase payments history on the desired page
    /// </summary>
    [PublicAPI]
    public class PaginatedVoucherPurchasePaymentsHistoryResponse : BasePagedResponse
    {
        /// <summary>
        /// List of voucher purchases history
        /// </summary>
        public IEnumerable<VoucherPurchasePaymentResponse> VoucherPurchasePaymentsHistory { get; set; }
    }
}
