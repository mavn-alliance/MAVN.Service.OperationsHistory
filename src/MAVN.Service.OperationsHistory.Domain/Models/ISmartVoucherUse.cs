using System;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public interface ISmartVoucherUse
    {
        string Id { get; set; }
        DateTime Timestamp { get; set; }
        Guid CustomerId { get; set; }
        Guid? LinkedCustomerId { get; set; }
        Guid PartnerId { get; set; }
        Guid? LocationId { get; set; }
        Guid CampaignId { get; set; }
        decimal Amount { get; set; }
        string AssetSymbol { get; set; }
    }
}
