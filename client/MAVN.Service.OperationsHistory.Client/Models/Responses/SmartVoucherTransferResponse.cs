using System;

namespace MAVN.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Response model for smart voucher transfer
    /// </summary>
    public class SmartVoucherTransferResponse
    {
        /// <summary>
        /// Id of the operation
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Id of the old owner
        /// </summary>
        public Guid OldCustomerId { get; set; }
        /// <summary>
        /// Id of the new owner
        /// </summary>
        public Guid NewCustomerId { get; set; }
        /// <summary>
        /// Id of the partner
        /// </summary>
        public Guid PartnerId { get; set; }
        /// <summary>
        /// Id of the campaign
        /// </summary>
        public string CampaignId { get; set; }
        /// <summary>
        /// Short code of the voucher
        /// </summary>
        public string ShortCode { get; set; }
        /// <summary>
        /// Price of the voucher
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Asset symbol of the currency
        /// </summary>
        public string AssetSymbol { get; set; }
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
        /// <summary>
        /// Email of the receiver
        /// </summary>
        public string OldCustomerEmail { get; set; }
        /// <summary>
        /// Email of the receiver
        /// </summary>
        public string NewCustomerEmail { get; set; }
    }
}
