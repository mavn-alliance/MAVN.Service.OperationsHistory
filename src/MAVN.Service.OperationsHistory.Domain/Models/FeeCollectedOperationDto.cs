using System;
using MAVN.Numerics;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class FeeCollectedOperationDto
    {
        public string OperationId { get; set; }
        
        public string CustomerId { get; set; }
        
        public Money18 Fee { get; set; }

        public FeeCollectionReason Reason { get; set; }
        
        public DateTime Timestamp { get; set; }
        
        public string AssetSymbol { get; set; }
    }
}
