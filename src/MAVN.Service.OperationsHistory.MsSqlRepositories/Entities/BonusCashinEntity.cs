using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Numerics;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("bonus_cash_in")]
    public class BonusCashInEntity : IBonusCashIn
    {
        [Key]
        [Column("transaction_id")]
        public string TransactionId { get; set; }

        [Column("external_operation_id")]
        public string ExternalOperationId { get; set; }

        [Column("customer_id")]
        [Required]
        public string CustomerId { get; set; }

        [Column("asset")]
        [Required]
        public string AssetSymbol { get; set; }

        [Column("amount")]
        [Required]
        public Money18 Amount { get; set; }

        [Column("location_code")]
        public string LocationCode { get; set; }

        [Column("bonus_type")]
        [Required]
        public string BonusType { get; set; }

        [Column("timestamp")]
        [Required]
        public DateTime Timestamp { get; set; }

        [Column("partner_id")]
        public string PartnerId { get; set; }

        [Column("location_id")]
        public string LocationId { get; set; }

        [Column("campaign_id")]
        [Required]
        public string CampaignId { get; set; }

        public CampaignEntity Campaign { get; set; }

        [Column("condition_name")]
        public string ConditionName { get; set; }

        [Column("condition_id")]
        public string ConditionId { get; set; }

        [Column("referral_id")]
        public string ReferralId { get; set; }

        public static BonusCashInEntity Create(IBonusCashIn bonusCashIn)
        {
            return new BonusCashInEntity
            {
                TransactionId = bonusCashIn.TransactionId,
                Timestamp = bonusCashIn.Timestamp,
                CustomerId = bonusCashIn.CustomerId,
                AssetSymbol = bonusCashIn.AssetSymbol,
                Amount = bonusCashIn.Amount,
                LocationCode = bonusCashIn.LocationCode,
                BonusType = bonusCashIn.BonusType,
                ExternalOperationId = bonusCashIn.ExternalOperationId,
                CampaignId = bonusCashIn.CampaignId,
                PartnerId = bonusCashIn.PartnerId,
                LocationId = bonusCashIn.LocationId,
                ConditionName = bonusCashIn.ConditionName,
                ConditionId = bonusCashIn.ConditionId,
                ReferralId = bonusCashIn.ReferralId
            };
        }
    }
}
