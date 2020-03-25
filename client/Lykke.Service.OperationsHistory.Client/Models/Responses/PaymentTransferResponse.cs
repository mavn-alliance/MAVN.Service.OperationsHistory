using System;
using Falcon.Numerics;
using JetBrains.Annotations;

namespace Lykke.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Response model for payment transferds
    /// </summary>
    [PublicAPI]
    public class PaymentTransferResponse
    {
        /// <summary>
        /// Id of the transfer
        /// </summary>
        public string TransferId { get; set; }
        /// <summary>
        /// Id of the customer
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// Id of the burn rule the which was paid for
        /// </summary>
        public string BurnRuleId { get; set; }
        /// <summary>
        /// Name of the burn rule
        /// </summary>
        public string BurnRuleName { get; set; }
        /// <summary>
        /// Id of the invoice
        /// </summary>
        public string InvoiceId { get; set; }
        /// <summary>
        /// Name of the Instalment
        /// </summary>
        public string InstalmentName { get; set; }

        /// <summary>
        /// Location code from SF
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// Amount of tokens paid
        /// </summary>
        public Money18 Amount { get; set; }
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
