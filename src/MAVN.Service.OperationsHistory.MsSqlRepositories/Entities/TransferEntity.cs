using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Numerics;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("transfer")]
    public class TransferEntity : ITransfer
    {
        [Key]
        [Column("transaction_id")]
        public string TransactionId { get; set; }

        [Column("external_operation_id")]
        public string ExternalOperationId { get; set; }

        [Column("sender_id")]
        [Required]
        public string SenderCustomerId { get; set; }

        [Column("sender_wallet_address")]
        [Required]
        public string SenderWalletAddress { get; set; }

        [Column("receiver_id")]
        [Required]
        public string ReceiverCustomerId { get; set; }

        [Column("receiver_wallet_address")]
        [Required]
        public string ReceiverWalletAddress { get; set; }

        [Column("asset")]
        [Required]
        public string AssetSymbol { get; set; }

        [Column("amount")]
        [Required]
        public Money18 Amount { get; set; }

        [Column("timestamp")]
        [Required]
        public DateTime Timestamp { get; set; }

        public static TransferEntity Create(ITransfer transfer)
        {
            return new TransferEntity
            {
                ReceiverCustomerId = transfer.ReceiverCustomerId,
                SenderCustomerId = transfer.SenderCustomerId,
                Amount = transfer.Amount,
                AssetSymbol = transfer.AssetSymbol,
                Timestamp = transfer.Timestamp,
                TransactionId = transfer.TransactionId,
                ExternalOperationId = transfer.ExternalOperationId,
                SenderWalletAddress = transfer.SenderWalletAddress,
                ReceiverWalletAddress = transfer.ReceiverWalletAddress
            };
        }
    }
}
