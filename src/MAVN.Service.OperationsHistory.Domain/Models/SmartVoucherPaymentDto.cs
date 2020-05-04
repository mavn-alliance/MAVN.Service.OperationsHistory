using System;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class SmartVoucherPaymentDto : ISmartVoucherPayment
    {
        public string PaymentRequestId { get; set; }
        public string ShortCode { get; set; }
        public Guid CustomerId { get; set; }
        public Guid PartnerId { get; set; }
        public Guid CampaignId { get; set; }
        public decimal Amount { get; set; }
        public string AssetSymbol { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
