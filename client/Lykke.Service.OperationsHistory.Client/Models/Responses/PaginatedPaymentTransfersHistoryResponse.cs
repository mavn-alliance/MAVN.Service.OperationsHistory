using System.Collections.Generic;
using JetBrains.Annotations;

namespace Lykke.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Gives the List of payment transfers history on the desired page
    /// </summary>
    [PublicAPI]
    public class PaginatedPaymentTransfersHistoryResponse : BasePagedResponse
    {
        /// <summary>
        /// List of payment transfers history
        /// </summary>
        public IEnumerable<PaymentTransferResponse> PaymentTransfersHistory { get; set; }
    }
}
