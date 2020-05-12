using System;
using MAVN.Numerics;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public interface IVoucherPurchasePayment
    {
        Guid TransferId { get; }

        Guid CustomerId { get; }

        Guid SpendRuleId { get; }

        Guid VoucherId { get; }

        Money18 Amount { get; }

        string AssetSymbol { get; }

        DateTime Timestamp { get; }
    }
}
