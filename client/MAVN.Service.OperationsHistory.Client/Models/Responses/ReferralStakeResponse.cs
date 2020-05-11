using System;
using MAVN.Numerics;

namespace MAVN.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Referral stake response model
    /// </summary>
    public class ReferralStakeResponse
    {
        /// <summary>
        /// Unique id of the referral operation
        /// </summary>
        public string ReferralId { get; set; }

        /// <summary>
        /// Id of the campaign
        /// </summary>
        public string CampaignId { get; set; }

        /// <summary>
        /// Name of the campaign
        /// </summary>
        public string CampaignName { get; set; }

        /// <summary>
        /// Id of the customer
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Asset symbol
        /// </summary>
        public string AssetSymbol { get; set; }

        /// <summary>
        /// Amount of tokens
        /// </summary>
        public Money18 Amount { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
