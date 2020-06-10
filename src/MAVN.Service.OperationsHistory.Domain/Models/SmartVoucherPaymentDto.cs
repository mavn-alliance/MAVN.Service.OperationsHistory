using System;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class SmartVoucherPaymentDto
    {
        public string PaymentRequestId { get; set; }
        public string ShortCode { get; set; }
        public Guid CustomerId { get; set; }
        public Guid PartnerId { get; set; }
        public string CampaignId { get; set; }
        public string
            CampaignName { get; set; }
        public string PartnerName { get; set; }
        public string Vertical { get; set; }
        public decimal Amount { get; set; }
        public string AssetSymbol { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
