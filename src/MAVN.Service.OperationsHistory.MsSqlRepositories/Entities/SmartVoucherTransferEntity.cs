using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("smart_voucher_transfers")]
    public class SmartVoucherTransferEntity
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Required]
        [Column("old_customer_id")]
        public Guid OldCustomerId { get; set; }

        [Required]
        [Column("new_customer_id")]
        public Guid NewCustomerId { get; set; }

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

        [Required]
        [Column("short_code")]
        public string ShortCode { get; set; }

        public static SmartVoucherTransferEntity Create(SmartVoucherTransferDto smartVoucherTransfer)
        {
            return new SmartVoucherTransferEntity
            {
                Timestamp = smartVoucherTransfer.Timestamp,
                PartnerId = smartVoucherTransfer.PartnerId,
                Amount = smartVoucherTransfer.Amount,
                AssetSymbol = smartVoucherTransfer.AssetSymbol,
                OldCustomerId = smartVoucherTransfer.OldCustomerId,
                NewCustomerId = smartVoucherTransfer.NewCustomerId,
                CampaignId = smartVoucherTransfer.CampaignId,
                Id = smartVoucherTransfer.Id,
                PartnerName = smartVoucherTransfer.PartnerName,
                Vertical = smartVoucherTransfer.Vertical,
                ShortCode = smartVoucherTransfer.ShortCode,
            };
        }
    }
}
