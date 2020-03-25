using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Numerics;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("payment_transfers")]
    public class PaymentTransferEntity : IPaymentTransfer
    {
        [Key]
        [Column("transfer_id")]
        public string TransferId { get; set; }

        [Required]
        [Column("customer_id")]
        public string CustomerId { get; set; }

        [Required]
        [Column("burn_rule_id")]
        public string BurnRuleId { get; set; }

        public BurnRuleEntity BurnRule { get; set; }

        [Required]
        [Column("invoice_id")]
        public string InvoiceId { get; set; }

        [Required]
        [Column("instalment_name")]
        public string InstalmentName { get; set; }

        [Required]
        [Column("location_code")]
        public string LocationCode { get; set; }

        [Required]
        [Column("asset_symbol")]
        public string AssetSymbol { get; set; }

        [Required]
        [Column("amount")]
        public Money18 Amount { get; set; }

        [Required]
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        public static PaymentTransferEntity Create(PaymentTransferDto paymentTransfer)
        {
            return new PaymentTransferEntity
            {
                Amount = paymentTransfer.Amount,
                Timestamp = paymentTransfer.Timestamp,
                CustomerId = paymentTransfer.CustomerId,
                BurnRuleId = paymentTransfer.BurnRuleId,
                InvoiceId = paymentTransfer.InvoiceId,
                TransferId = paymentTransfer.TransferId,
                AssetSymbol = paymentTransfer.AssetSymbol,
                InstalmentName = paymentTransfer.InstalmentName,
                LocationCode = paymentTransfer.LocationCode,
            };
        }
    }
}
