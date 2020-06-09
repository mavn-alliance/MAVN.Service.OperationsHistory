using System;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class SmartVoucherUseDto : ISmartVoucherUse
    {
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? LinkedCustomerId { get; set; }
        public Guid PartnerId { get; set; }
        public Guid? LocationId { get; set; }
        public Guid CampaignId { get; set; }
        public decimal Amount { get; set; }
        public string AssetSymbol { get; set; }
    }
}
