using System;
using JetBrains.Annotations;

namespace Lykke.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Represents a history of a completed transaction
    /// </summary>
    [PublicAPI]
    public class TransactionHistoryResponse
    {
        /// <summary>
        /// Id of the customer
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Id of the transaction
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Asset symbol
        /// </summary>
        public string AssetSymbol { get; set; }

        /// <summary>
        /// Timestamp of the transaction
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Type of the transaction. For example - transfer
        /// </summary>
        public string Type { get; set; }
    }
}
