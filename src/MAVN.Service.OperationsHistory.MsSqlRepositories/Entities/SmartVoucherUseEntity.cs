using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("smart_voucher_uses")]
    public class SmartVoucherUseEntity
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Required]
        [Column("customer_id")]
        public Guid CustomerId { get; set; }

        [Column("linked_customer_id")]
        public Guid? LinkedCustomerId { get; set; }

        [Required]
        [Column("partner_id")]
        public Guid PartnerId { get; set; }

        [Column("partner_name")]
        public string PartnerName { get; set; }

        [Column("vertical")]
        public string Vertical { get; set; }

        [Column("location_id")]
        public Guid? LocationId { get; set; }

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

        public static SmartVoucherUseEntity Create(SmartVoucherUseDto smartVoucherUse)
        {
            return new SmartVoucherUseEntity
            {
                Timestamp = smartVoucherUse.Timestamp,
                PartnerId = smartVoucherUse.PartnerId,
                Amount = smartVoucherUse.Amount,
                AssetSymbol = smartVoucherUse.AssetSymbol,
                CustomerId = smartVoucherUse.CustomerId,
                CampaignId = smartVoucherUse.CampaignId,
                LinkedCustomerId = smartVoucherUse.LinkedCustomerId,
                Id = smartVoucherUse.Id,
                LocationId = smartVoucherUse.LocationId,
                Vertical = smartVoucherUse.Vertical,
                PartnerName = smartVoucherUse.PartnerName,
            };
        }
    }
}
