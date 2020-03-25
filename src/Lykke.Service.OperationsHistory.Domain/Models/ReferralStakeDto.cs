using System;
using Falcon.Numerics;

namespace Lykke.Service.OperationsHistory.Domain.Models
{
    public class ReferralStakeDto
    {
        public string ReferralId { get; set; }

        public string CampaignId { get; set; }

        public string CampaignName { get; set; }

        public string CustomerId { get; set; }

        public string AssetSymbol { get; set; }

        public Money18 Amount { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
