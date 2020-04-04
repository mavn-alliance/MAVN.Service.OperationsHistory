using System.Collections.Generic;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class PaginatedCustomerOperationsModel : BasePagedModel
    {
        public IEnumerable<BonusCashInDto> BonusCashIns { get; set; }

        public IEnumerable<Transfer> Transfers { get; set; }

        public IEnumerable<PaymentTransferDto> PaymentTransfers { get; set; }

        public IEnumerable<IPartnersPayment> PartnersPayments { get; set; }

        public IEnumerable<PaymentTransferDto> RefundedPaymentTransfers { get; set; }

        public IEnumerable<IPartnersPayment> RefundedPartnersPayments { get; set; }

        public IEnumerable<LinkedWalletTransferDto> LinkedWalletTransfers { get; set; }

        public IEnumerable<ReferralStakeDto> ReferralStakes { get; set; }

        public IEnumerable<ReferralStakeDto> ReleasedReferralStakes { get; set; }

        public IEnumerable<FeeCollectedOperationDto> FeeCollectedOperations { get; set; }

        public IEnumerable<VoucherPurchasePaymentDto> VoucherPurchasePayments { get; set; }
    }
}
