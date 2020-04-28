using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Returns paged list of smart voucher payments
    /// </summary>
    [PublicAPI]
    public class PaginatedSmartVoucherPaymentsResponse : BasePagedResponse
    {
        /// <summary>
        /// List of voucher purchases history
        /// </summary>
        public IEnumerable<SmartVoucherPaymentResponse> SmartVoucherPaymentsHistory { get; set; }
    }
}
