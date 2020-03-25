using System;
using Falcon.Numerics;

namespace Lykke.Service.OperationsHistory.Domain.Models
{
    public interface IBonusCashIn
    {
        string TransactionId { get; }

        string ExternalOperationId { get; }

        string CustomerId { get; }

        string PartnerId { get; }

        string LocationId { get; }

        string CampaignId { get; }

        string AssetSymbol { get; }

        Money18 Amount { get; }

        string LocationCode { get; }

        string BonusType { get; }

        DateTime Timestamp { get; }

        string ConditionId { get; }

        string ConditionName { get; }

        string ReferralId { get; }
    }
}
