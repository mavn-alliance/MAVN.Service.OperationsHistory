using System;
using Falcon.Numerics;

namespace Lykke.Service.OperationsHistory.Domain.Models
{
    public class PaymentTransferDto : IPaymentTransfer
    {
        public string TransferId { get; set; }
        public string CustomerId { get; set; }
        public string BurnRuleId { get; set; }
        public string BurnRuleName { get; set; }
        public string InvoiceId { get; set; }
        public string InstalmentName { get; set; }
        public Money18 Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string AssetSymbol { get; set; }
        public string LocationCode { get; set; }
    }
}
