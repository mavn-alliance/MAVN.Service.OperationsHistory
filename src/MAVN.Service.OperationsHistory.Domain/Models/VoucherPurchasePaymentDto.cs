using System;
using MAVN.Numerics;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class VoucherPurchasePaymentDto : IVoucherPurchasePayment
    {
        public Guid TransferId { get; set; }

        public Guid CustomerId { get; set; }

        public Guid SpendRuleId { get; set; }

        public Guid VoucherId { get; set; }

        public Money18 Amount { get; set; }

        public string AssetSymbol { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
