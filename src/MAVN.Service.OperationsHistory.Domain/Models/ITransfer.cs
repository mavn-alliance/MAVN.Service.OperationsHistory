using System;
using Falcon.Numerics;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public interface ITransfer
    {
        string TransactionId { get; set; }

        string ExternalOperationId { get; set; }

        string SenderCustomerId { get; set; }
        
        string SenderWalletAddress { get; set; }

        string ReceiverCustomerId { get; set; }
        
        string ReceiverWalletAddress { get; set; }

        string AssetSymbol { get; set; }

        Money18 Amount { get; set; }

        DateTime Timestamp { get; set; }
    }
}
