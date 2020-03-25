using System;
using Falcon.Numerics;

namespace Lykke.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Linked wallet transfer response
    /// </summary>
    public class LinkedWalletTransferResponse
    {
        /// <summary>
        /// Internal id of the transaction
        /// </summary>
        public string TransactionId { get; set; }

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
        /// Linked public wallet blockchain address
        /// </summary>
        public string LinkedWalletAddress { get; set; }

        /// <summary>
        /// The direction of the transfer
        /// </summary>
        public LinkedWalletTransferDirection Direction { get; set; }
    }
}
