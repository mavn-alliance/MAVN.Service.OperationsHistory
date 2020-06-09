using System.Collections.Generic;

namespace MAVN.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Paginated response of customer operations like transfers and bonuses
    /// </summary>
    public class PaginatedCustomerOperationsResponse : BasePagedResponse
    {
        /// <summary>
        /// List of transfers
        /// </summary>
        public IEnumerable<TransferResponse> Transfers { get; set; }

        /// <summary>
        /// List of bonus cash ins
        /// </summary>
        public IEnumerable<BonusCashInResponse> BonusCashIns { get; set; }

        /// <summary>
        /// List of partner payments
        /// </summary>
        public IEnumerable<PartnersPaymentResponse> PartnersPayments { get; set; }

        /// <summary>
        /// List of refunded partner payments
        /// </summary>
        public IEnumerable<PartnersPaymentResponse> RefundedPartnersPayments { get; set; }

        /// <summary>
        /// List of referral stakes
        /// </summary>
        public IEnumerable<ReferralStakeResponse> ReferralStakes { get; set; }

        /// <summary>
        /// List of released referral stakes
        /// </summary>
        public IEnumerable<ReferralStakeResponse> ReleasedReferralStakes { get; set; }
        
        /// <summary>
        /// List of transfers to or from linked public wallet address
        /// </summary>
        public IEnumerable<LinkedWalletTransferResponse> LinkedWalletTransfers { get; set; }

        /// <summary>
        /// List of transfers to or from linked public wallet address
        /// </summary>
        public IEnumerable<FeeCollectedOperationResponse> FeeCollectedOperations { get; set; }

        /// <summary>
        /// List of voucher purchase payments.
        /// </summary>
        public IEnumerable<VoucherPurchasePaymentResponse> VoucherPurchasePayments { get; set; }

        /// <summary>
        /// List of smart voucher payments.
        /// </summary>
        public IEnumerable<SmartVoucherPaymentResponse> SmartVoucherPayments { get; set; }

        /// <summary>
        /// List of smart voucher uses.
        /// </summary>
        public IEnumerable<SmartVoucherUseResponse> SmartVoucherUses { get; set; }
    }
}
