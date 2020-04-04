using System;
using System.Security.AccessControl;
using Falcon.Numerics;
using JetBrains.Annotations;

namespace MAVN.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Response model for history of Bonus Cash In operation
    /// </summary>
    [PublicAPI]
    public class BonusCashInResponse
    {
        /// <summary>Internal Id of the transaction</summary>
        public string TransactionId { get; set; }

        /// <summary>External Id of the operation</summary>
        public string ExternalOperationId { get; set; }

        /// <summary>Id of the customer</summary>
        public string CustomerId { get; set; }

        /// <summary>Asset symbol</summary>
        public string AssetSymbol { get; set; }

        /// <summary>Received amount</summary>
        public Money18 Amount { get; set; }

        /// <summary>Location code</summary>
        public string LocationCode { get; set; }

        /// <summary>Type of the bonus</summary>
        public string BonusType { get; set; }

        /// <summary>Timestamp of the event</summary>
        public DateTime Timestamp { get; set; }

        /// <summary>Id of the partner</summary>
        public string PartnerId { get; set; }

        /// <summary>Id of the location</summary>
        public string LocationId { get; set; }

        /// <summary>Name of the campaign</summary>
        public string CampaignName { get; set; }

        /// <summary>Campaign id</summary>
        public string CampaignId { get; set; }

        /// <summary>Name of the condition</summary>
        public string ConditionName { get; set; }

        /// <summary>Id of the condition</summary>
        public string ConditionId { get; set; }

        /// <summary>Id of a referral</summary>
        public string ReferralId { get; set; }
    }
}
