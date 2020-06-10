using System;
using JetBrains.Annotations;

namespace MAVN.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Response model for smart voucher payment operation
    /// </summary>
    [PublicAPI]
    public class SmartVoucherPaymentResponse
    {
        /// <summary>
        /// The id of the payment request
        /// </summary>
        public string PaymentRequestId { get; set; }
        /// <summary>
        /// Short code of the voucher
        /// </summary>
        public string ShortCode { get; set; }
        /// <summary>
        /// The id of the campaign
        /// </summary>
        public string CampaignId { get; set; }
        /// <summary>
        /// The id of the customer
        /// </summary>
        public Guid CustomerId { get; set; }
        /// <summary>
        /// The id of the partner
        /// </summary>
        public Guid PartnerId { get; set; }
        /// <summary>
        /// Amount paid
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Asset symbol
        /// </summary>
        public string AssetSymbol { get; set; }
        /// <summary>
        /// Timestamp of the payment
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Name of the campaign
        /// </summary>
        public string CampaignName { get; set; }
        /// <summary>
        /// Name of the partner
        /// </summary>
        public string PartnerName { get; set; }
        /// <summary>
        /// Vertical of the partner
        /// </summary>
        public string Vertical { get; set; }
    }
}
