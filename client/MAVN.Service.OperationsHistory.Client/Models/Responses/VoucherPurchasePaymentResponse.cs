using System;
using MAVN.Numerics;

namespace MAVN.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Represents voucher purchase payment.
    /// </summary>
    public class VoucherPurchasePaymentResponse
    {
        /// <summary>
        /// The payment transfer identifier.
        /// </summary>
        public Guid TransferId { get; set; }

        /// <summary>
        /// The customer identifier.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// The spend rule identifier.
        /// </summary>
        public Guid SpendRuleId { get; set; }

        /// <summary>
        /// The sold voucher identifier.
        /// </summary>
        public Guid VoucherId { get; set; }

        /// <summary>
        /// The amount of tokens paid.
        /// </summary>
        public Money18 Amount { get; set; }

        /// <summary>
        /// The currency code.
        /// </summary>
        public string AssetSymbol { get; set; }
        
        /// <summary>
        /// The date and time of transfer.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
