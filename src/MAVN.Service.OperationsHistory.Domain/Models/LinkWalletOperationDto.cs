using System;
using Falcon.Numerics;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class LinkWalletOperationDto
    {
        public string OperationId { get; set; }

        public string CustomerId { get; set; }

        public Money18 Fee { get; set; }

        public string PrivateAddress { get; set; }

        public string PublicAddress { get; set; }

        public LinkWalletDirection Direction { get; set; }

        public DateTime Timestamp { get; set; }

        public string AssetSymbol { get; set; }
    }
}
