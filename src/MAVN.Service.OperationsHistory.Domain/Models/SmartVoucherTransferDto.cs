using System;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class SmartVoucherTransferDto
    {
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid OldCustomerId { get; set; }
        public Guid NewCustomerId { get; set; }
        public Guid PartnerId { get; set; }
        public string CampaignId { get; set; }
        public string ShortCode { get; set; }
        public decimal Amount { get; set; }
        public string AssetSymbol { get; set; }
        public string CampaignName { get; set; }
        public string PartnerName { get; set; }
        public string Vertical { get; set; }
        public string NewCustomerEmail { get; set; }
        public string OldCustomerEmail { get; set; }
    }
}
