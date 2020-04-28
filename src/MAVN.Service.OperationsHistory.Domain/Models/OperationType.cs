namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public enum OperationType
    {
        BonusCashIn,
        P2PTransfer,
        PaymentTransferTokensReserved,
        PaymentTransferRefunded,
        PartnersPaymentTokensReserved,
        PartnersPaymentRefunded,
        ReferralStakeTokensReserved,
        ReferralStakeTokensReleased,
        LinkedWalletOutgoingTransfer,
        LinkedWalletIncomingTransfer,
        FeeCollected,
        VoucherPurchasePayment,
        SmartVoucherPayment
    }
}
