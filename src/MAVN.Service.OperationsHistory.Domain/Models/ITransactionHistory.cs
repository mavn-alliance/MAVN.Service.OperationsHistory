using System;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public interface ITransactionHistory
    {
        string CustomerId { get; set; }

        string TransactionId { get; set; }

        string AssetSymbol { get; set; }

        DateTime Timestamp { get; set; }

        string Type { get; set; }
    }
}
