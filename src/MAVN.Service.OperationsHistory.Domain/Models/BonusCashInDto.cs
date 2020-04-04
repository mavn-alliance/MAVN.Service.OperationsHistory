using System;
using Falcon.Numerics;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class BonusCashInDto : IBonusCashIn
    {
        public string TransactionId { get; set; }

        public string ExternalOperationId { get; set; }

        public string CustomerId { get; set; }

        public string PartnerId { get; set; }

        public string LocationId { get; set; }

        public string CampaignId { get; set; }

        public string AssetSymbol { get; set; }

        public Money18 Amount { get; set; }

        public string LocationCode { get; set; }

        public string BonusType { get; set; }

        public DateTime Timestamp { get; set; }

        public string CampaignName { get; set; }

        public string ConditionId { get; set; }

        public string ConditionName { get; set; }

        public string ReferralId { get; set; }
    }
}
