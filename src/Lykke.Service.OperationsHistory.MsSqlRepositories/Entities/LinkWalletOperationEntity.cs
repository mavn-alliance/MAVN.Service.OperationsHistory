using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Falcon.Numerics;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("link_wallet_operations")]
    public class LinkWalletOperationEntity
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("customer_id")]
        [Required]
        public string CustomerId { get; set; }

        [Column("private_address")]
        [Required]
        public string PrivateAddress { get; set; }

        [Column("public_address")]
        public string PublicAddress { get; set; }

        [Column("timestamp")]
        [Required]
        public DateTime Timestamp { get; set; }

        [Column("direction")]
        [Required]
        public LinkWalletDirection Direction { get; set; }

        [Column("fee")]
        public Money18 Fee { get; set; }

        public static LinkWalletOperationEntity Create(LinkWalletOperationDto operation)
        {
            return new LinkWalletOperationEntity
            {
                Id = operation.OperationId,
                Direction = operation.Direction,
                Fee = operation.Fee,
                CustomerId = operation.CustomerId,
                PrivateAddress = operation.PrivateAddress,
                PublicAddress = operation.PublicAddress,
                Timestamp = operation.Timestamp
            };
        }
    }
}
