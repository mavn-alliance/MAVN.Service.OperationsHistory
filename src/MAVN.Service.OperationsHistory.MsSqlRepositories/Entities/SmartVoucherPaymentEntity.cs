using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("smart_voucher_payments")]
    public class SmartVoucherPaymentEntity
    {
        [Key]
        [Column("payment_request_id")]
        public string PaymentRequestId { get; set; }

        [Required]
        [Column("short_code")]
        public string ShortCode { get; set; }

        [Required]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Required]
        [Column("partner_id")]
        public Guid PartnerId { get; set; }

        [Column("partner_name")]
        public string PartnerName { get; set; }

        [Column("vertical")]
        public string Vertical { get; set; }

        [Required]
        [Column("campaign_id")]
        public string CampaignId { get; set; }

        public CampaignEntity Campaign { get; set; }

        [Required]
        [Column("amount")]
        public decimal Amount { get; set; }

        [Required]
        [Column("asset_symbol")]
        public string AssetSymbol { get; set; }

        [Required]
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        public static SmartVoucherPaymentEntity Create(SmartVoucherPaymentDto payment)
        {
            return new SmartVoucherPaymentEntity
            {
                Timestamp = payment.Timestamp,
                PartnerId = payment.PartnerId,
                Amount = payment.Amount,
                ShortCode = payment.ShortCode,
                AssetSymbol = payment.AssetSymbol,
                CustomerId = payment.CustomerId,
                CampaignId = payment.CampaignId,
                PaymentRequestId = payment.PaymentRequestId,
                Vertical = payment.Vertical,
                PartnerName = payment.PartnerName,
            };
        }
    }
}
