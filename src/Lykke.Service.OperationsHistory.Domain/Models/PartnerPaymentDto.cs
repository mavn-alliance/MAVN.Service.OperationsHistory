using System;
using Falcon.Numerics;

namespace Lykke.Service.OperationsHistory.Domain.Models
{
    public class PartnerPaymentDto : IPartnersPayment
    {
        public string PaymentRequestId { get; set; }

        public string CustomerId { get; set; }

        public string PartnerId { get; set; }

        public string PartnerName { get; set; }

        public string LocationId { get; set; }

        public Money18 Amount { get; set; }

        public DateTime Timestamp { get; set; }

        public string AssetSymbol { get; set; }
    }
}
