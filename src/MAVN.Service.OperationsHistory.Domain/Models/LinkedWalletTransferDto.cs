using System;
using MAVN.Numerics;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class LinkedWalletTransferDto
    {
        public string OperationId { get; set; }
        
        public string CustomerId { get; set; }
        
        public Money18 Amount { get; set; }
        
        public string PrivateAddress { get; set; }
        
        public string PublicAddress { get; set; }
        
        public LinkedWalletTransferDirection Direction { get; set; }
        
        public DateTime Timestamp { get; set; }
        
        public string AssetSymbol { get; set; }
    }
}
