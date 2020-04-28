using System;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public interface ISmartVoucherPayment
    {
        string PaymentRequestId { get; set; }
        Guid CustomerId { get; set; }
        Guid PartnerId { get; set; }
        decimal Amount { get; set; }
        string AssetSymbol { get; set; }
        DateTime Timestamp { get; set; }
    }
}
