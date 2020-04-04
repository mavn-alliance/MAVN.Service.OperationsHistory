using System;
using Falcon.Numerics;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public interface IPaymentTransfer
    {
        string TransferId { get; }
        string CustomerId { get; }
        string BurnRuleId { get; }
        string InvoiceId { get; }
        Money18 Amount { get; }
        DateTime Timestamp { get; }
        string InstalmentName { get; }
        string AssetSymbol { get; }
        string LocationCode { get; }
    }
}
