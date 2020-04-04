using System;
using Falcon.Numerics;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class Transfer : ITransfer
    {
        public string TransactionId { get; set; }

        public string ExternalOperationId { get; set; }

        public string SenderCustomerId { get; set; }
        
        public string SenderWalletAddress { get; set; }

        public string ReceiverCustomerId { get; set; }
        
        public string ReceiverWalletAddress { get; set; }

        public string SenderCustomerEmail { get; set; }

        public string ReceiverCustomerEmail { get; set; }

        public string AssetSymbol { get; set; }

        public Money18 Amount { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
