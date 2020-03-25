using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Numerics;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("refunded_partners_payments")]
    public class PartnersPaymentRefundEntity : IPartnersPayment
    {
        [Key]
        [Required]
        [Column("payment_request_id")]
        public string PaymentRequestId { get; set; }

        [Required]
        [Column("customer_id")]
        public string CustomerId { get; set; }

        [Required]
        [Column("partner_id")]
        public string PartnerId { get; set; }

        [Required]
        [Column("partner_name")]
        public string PartnerName { get; set; }

        [Column("location_id")]
        public string LocationId { get; set; }

        [Required]
        [Column("amount")]
        public Money18 Amount { get; set; }

        [Required]
        [Column("asset_symbol")]
        public string AssetSymbol { get; set; }

        [Required]
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        public static PartnersPaymentRefundEntity Create(PartnerPaymentDto partnerPayment)
        {
            return new PartnersPaymentRefundEntity
            {
                Amount = partnerPayment.Amount,
                CustomerId = partnerPayment.CustomerId,
                Timestamp = partnerPayment.Timestamp,
                PartnerId = partnerPayment.PartnerId,
                PaymentRequestId = partnerPayment.PaymentRequestId,
                LocationId = partnerPayment.LocationId,
                AssetSymbol = partnerPayment.AssetSymbol,
                PartnerName = partnerPayment.PartnerName,
            };
        }
    }
}
