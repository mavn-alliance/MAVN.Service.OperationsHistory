using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("smart_voucher_payments")]
    public class SmartVoucherPaymentEntity : ISmartVoucherPayment
    {
        [Key]
        public string PaymentRequestId { get; set; }

        [Required]
        public string ShortCode { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid PartnerId { get; set; }

        [Required]
        public Guid CampaignId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string AssetSymbol { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public static SmartVoucherPaymentEntity Create(ISmartVoucherPayment payment)
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
            };
        }
    }
}
