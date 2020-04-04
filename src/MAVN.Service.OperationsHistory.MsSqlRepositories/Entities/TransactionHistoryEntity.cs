using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("transaction_history")]
    public class TransactionHistoryEntity : ITransactionHistory
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("customer_id")]
        [Required]
        public string CustomerId { get; set; }

        [Column("transaction_id")]
        [Required]
        public string TransactionId { get; set; }

        [Column("asset")]
        [Required]
        public string AssetSymbol { get; set; }

        [Column("timestamp")]
        [Required]
        public DateTime Timestamp { get; set; }

        [Column("type")]
        [Required]
        public string Type { get; set; }

        public static TransactionHistoryEntity CreateForSender(ITransfer transfer)
        {
            return new TransactionHistoryEntity
            {
                CustomerId = transfer.SenderCustomerId,
                Timestamp = transfer.Timestamp,
                TransactionId = transfer.TransactionId,
                AssetSymbol = transfer.AssetSymbol,
                Type = OperationType.P2PTransfer.ToString(),
            };
        }

        public static TransactionHistoryEntity CreateForReceiver(ITransfer transfer)
        {
            return new TransactionHistoryEntity
            {
                CustomerId = transfer.ReceiverCustomerId,
                Timestamp = transfer.Timestamp,
                TransactionId = transfer.TransactionId,
                AssetSymbol = transfer.AssetSymbol,
                Type = OperationType.P2PTransfer.ToString(),
            };
        }

        public static TransactionHistoryEntity Create(BonusCashInDto bonusCashIn)
        {
            return new TransactionHistoryEntity
            {
                CustomerId = bonusCashIn.CustomerId,
                Timestamp = bonusCashIn.Timestamp,
                TransactionId = bonusCashIn.TransactionId,
                AssetSymbol = bonusCashIn.AssetSymbol,
                Type = OperationType.BonusCashIn.ToString(),
            };
        }

        public static TransactionHistoryEntity CreateForPaymentTransferTokensReservation(PaymentTransferDto paymentTransfer)
        {
            return new TransactionHistoryEntity
            {
                CustomerId = paymentTransfer.CustomerId,
                Timestamp = paymentTransfer.Timestamp,
                TransactionId = paymentTransfer.TransferId,
                AssetSymbol = paymentTransfer.AssetSymbol,
                Type = OperationType.PaymentTransferTokensReserved.ToString(),
            };
        }

        public static TransactionHistoryEntity CreateForPaymentTransferRefund(PaymentTransferDto paymentTransfer)
        {
            return new TransactionHistoryEntity
            {
                CustomerId = paymentTransfer.CustomerId,
                Timestamp = paymentTransfer.Timestamp,
                TransactionId = paymentTransfer.TransferId,
                AssetSymbol = paymentTransfer.AssetSymbol,
                Type = OperationType.PaymentTransferRefunded.ToString(),
            };
        }

        public static TransactionHistoryEntity CreateForPartnerPaymentTokensReservation(PartnerPaymentDto partnerPayment)
        {
            return new TransactionHistoryEntity
            {
                CustomerId = partnerPayment.CustomerId,
                Timestamp = partnerPayment.Timestamp,
                TransactionId = partnerPayment.PaymentRequestId,
                AssetSymbol = partnerPayment.AssetSymbol,
                Type = OperationType.PartnersPaymentTokensReserved.ToString(),
            };
        }

        public static TransactionHistoryEntity CreateForPartnersPaymentRefund(PartnerPaymentDto partnerPayment)
        {
            return new TransactionHistoryEntity
            {
                CustomerId = partnerPayment.CustomerId,
                Timestamp = partnerPayment.Timestamp,
                TransactionId = partnerPayment.PaymentRequestId,
                AssetSymbol = partnerPayment.AssetSymbol,
                Type = OperationType.PartnersPaymentRefunded.ToString(),
            };
        }

        public static TransactionHistoryEntity CreateForReferralStakeTokensReservation(ReferralStakeDto referralStake)
        {
            return new TransactionHistoryEntity
            {
                CustomerId = referralStake.CustomerId,
                Timestamp = referralStake.Timestamp,
                TransactionId = referralStake.ReferralId,
                AssetSymbol = referralStake.AssetSymbol,
                Type = OperationType.ReferralStakeTokensReserved.ToString(),
            };
        }

        public static TransactionHistoryEntity CreateForReferralStakeTokensRelease(ReferralStakeDto referralStake)
        {
            return new TransactionHistoryEntity
            {
                CustomerId = referralStake.CustomerId,
                Timestamp = referralStake.Timestamp,
                TransactionId = referralStake.ReferralId,
                AssetSymbol = referralStake.AssetSymbol,
                Type = OperationType.ReferralStakeTokensReleased.ToString(),
            };
        }

        public static TransactionHistoryEntity CreateForLinkedWalletTransfer(LinkedWalletTransferDto transfer)
        {
            return new TransactionHistoryEntity
            {
                CustomerId = transfer.CustomerId,
                Timestamp = transfer.Timestamp,
                Type = transfer.Direction == LinkedWalletTransferDirection.Incoming
                    ? OperationType.LinkedWalletIncomingTransfer.ToString()
                    : OperationType.LinkedWalletOutgoingTransfer.ToString(),
                AssetSymbol = transfer.AssetSymbol,
                TransactionId = transfer.OperationId
            };
        }
        
        public static TransactionHistoryEntity Create(FeeCollectedOperationDto operation)
        {
            return new TransactionHistoryEntity
            {
                CustomerId = operation.CustomerId,
                Timestamp = operation.Timestamp,
                Type = OperationType.FeeCollected.ToString(),
                AssetSymbol = operation.AssetSymbol,
                TransactionId = operation.OperationId
            };
        }

        public static TransactionHistoryEntity CreateForVoucherTokenReservation(VoucherPurchasePaymentDto operation)
        {
            return new TransactionHistoryEntity
            {
                CustomerId = operation.CustomerId.ToString(),
                Timestamp = operation.Timestamp,
                Type = OperationType.VoucherPurchasePayment.ToString(),
                AssetSymbol = operation.AssetSymbol,
                TransactionId = operation.TransferId.ToString()
            };
        }
    }
}
