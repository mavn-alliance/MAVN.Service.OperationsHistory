using System.Threading.Tasks;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.Domain.Services
{
    public interface IOperationsService
    {
        Task ProcessTransferEventAsync(ITransfer transfer);

        Task ProcessBonusReceivedEventAsync(BonusCashInDto bonusCashIn);

        Task ProcessPaymentTransferTokensReservedEventAsync(PaymentTransferDto paymentTransferDto);

        Task ProcessCustomerTierChangedEventAsync(CustomerTierModel customerTier);

        Task ProcessPartnersPaymentTokensReservedEventAsync(PartnerPaymentDto partnerPayment);

        Task ProcessRefundPaymentTransferEventAsync(PaymentTransferDto paymentTransferDto);

        Task ProcessRefundPartnersPaymentEventAsync(PartnerPaymentDto partnerPayment);

        Task ProcessReferralStakeTokensReservationEventAsync(ReferralStakeDto referralStake);

        Task ProcessReferralStakeTokensReleaseEventAsync(ReferralStakeDto referralStake);

        Task ProcessTransferToLinkedEvent(LinkedWalletTransferDto transfer);

        Task ProcessTransferFromLinkedToInternalEvent(LinkedWalletTransferDto transfer);

        Task ProcessFeeCollectedAsync(FeeCollectedOperationDto feeCollectedOperation);

        Task ProcessWalletLinkingStateChangeCompletedEventAsync(LinkWalletOperationDto linkWalletOperation);

        Task ProcessVoucherTokensUsedEventAsync(VoucherPurchasePaymentDto voucherPurchasePaymentOperation);
    }
}
