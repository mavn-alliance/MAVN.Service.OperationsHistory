using System;
using Falcon.Numerics;

namespace Lykke.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Response model for link wallet operation
    /// </summary>
    public class FeeCollectedOperationResponse
    {
        /// <summary>
        /// Id of the operation
        /// </summary>
        public string OperationId { get; set; }

        /// <summary>
        /// Id of the customer
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Fee for the operation
        /// </summary>
        public Money18 Fee { get; set; }

        /// <summary>
        /// Reason for the fee collection
        /// </summary>
        public FeeCollectionReason Reason { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Asset symbol
        /// </summary>
        public string AssetSymbol { get; set; }
    }
}
