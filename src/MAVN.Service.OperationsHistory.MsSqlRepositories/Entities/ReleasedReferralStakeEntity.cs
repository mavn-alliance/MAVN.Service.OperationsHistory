using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Numerics;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("released_referral_stakes")]
    public class ReleasedReferralStakeEntity
    {
        [Key]
        [Column("referral_id")]
        public string ReferralId { get; set; }


        [Column("campaign_id")]
        [Required]
        public string CampaignId { get; set; }

        public CampaignEntity Campaign { get; set; }

        [Required]
        [Column("customer_id")]
        public string CustomerId { get; set; }

        [Column("asset")]
        [Required]
        public string AssetSymbol { get; set; }

        [Required]
        [Column("amount")]
        public Money18 Amount { get; set; }

        [Required]
        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        public static ReleasedReferralStakeEntity Create(ReferralStakeDto referralStake)
        {
            return new ReleasedReferralStakeEntity
            {
                CampaignId = referralStake.CampaignId,
                CustomerId = referralStake.CustomerId,
                Timestamp = referralStake.Timestamp,
                ReferralId = referralStake.ReferralId,
                Amount = referralStake.Amount,
                AssetSymbol = referralStake.AssetSymbol,
            };
        }
    }
}
