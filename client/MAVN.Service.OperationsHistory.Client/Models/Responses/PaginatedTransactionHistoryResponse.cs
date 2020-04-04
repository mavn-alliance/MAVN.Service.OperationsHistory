using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Gives the List of Transaction History on the desired page
    /// </summary>
    [PublicAPI]
    public class PaginatedTransactionHistoryResponse : BasePagedResponse
    {
        /// <summary>
        /// List of Transaction History
        /// </summary>
        public IEnumerable<TransactionHistoryResponse> TransactionsHistory { get; set; }
    }
}
