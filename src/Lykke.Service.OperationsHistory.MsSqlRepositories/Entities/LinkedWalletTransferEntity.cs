using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Numerics;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("linked_wallet_transfer")]
    public class LinkedWalletTransferEntity
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("customer_id")]
        [Required]
        public string CustomerId { get; set; }

        [Column("amount")]
        [Required]
        public Money18 Amount { get; set; }

        [Column("private_address")]
        [Required]
        public string PrivateAddress { get; set; }

        [Column("public_address")]
        [Required]
        public string PublicAddress { get; set; }
        
        [Column("timestamp")]
        [Required]
        public DateTime Timestamp { get; set; }
        
        [Column("direction")]
        [Required]
        public LinkedWalletTransferDirection Direction { get; set; }
        
        [Column("asset")]
        [Required]
        public string AssetSymbol { get; set; }

        public static LinkedWalletTransferEntity Create(LinkedWalletTransferDto transfer)
        {
            return new LinkedWalletTransferEntity
            {
                Id = transfer.OperationId,
                Amount = transfer.Amount,
                Direction = transfer.Direction,
                CustomerId = transfer.CustomerId,
                PrivateAddress = transfer.PrivateAddress,
                PublicAddress = transfer.PublicAddress,
                Timestamp = transfer.Timestamp,
                AssetSymbol = transfer.AssetSymbol
            };
        }
    }
}
