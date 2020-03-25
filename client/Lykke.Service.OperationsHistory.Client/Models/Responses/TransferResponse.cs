using System;
using Falcon.Numerics;

namespace Lykke.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Response model for history of transfer operation
    /// </summary>
    public class TransferResponse
    {
        /// <summary>
        /// Internal id of the transaction
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// External id of the operation
        /// </summary>
        public string ExternalOperationId { get; set; }

        /// <summary>
        /// Id of the sender
        /// </summary>
        public string SenderCustomerId { get; set; }

        /// <summary>
        /// Id of the receiver
        /// </summary>
        public string ReceiverCustomerId { get; set; }

        /// <summary>
        /// Asset symbol
        /// </summary>
        public string AssetSymbol { get; set; }

        /// <summary>
        /// Amount that was transferred
        /// </summary>
        public Money18 Amount { get; set; }

        /// <summary>
        /// Timestamp of the event
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Blockchain address of the wallet
        /// </summary>
        public string WalletAddress { get; set; }
        
        /// <summary>
        /// Blockchain address of the wallet of the other side of transfer
        /// </summary>
        public string OtherSideWalletAddress { get; set; }

        /// <summary>
        /// Email of the sender
        /// </summary>
        public string SenderCustomerEmail { get; set; }

        /// <summary>
        /// Email of the receiver
        /// </summary>
        public string ReceiverCustomerEmail { get; set; }
    }
}
