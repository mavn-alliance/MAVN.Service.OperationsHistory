using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MAVN.Numerics;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("fee_collected_operations")]
    public class FeeCollectedOperationEntity
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("customer_id")]
        [Required]
        public string CustomerId { get; set; }
        
        [Column("timestamp")]
        [Required]
        public DateTime Timestamp { get; set; }
        
        [Column("reason")]
        [Required]
        public FeeCollectionReason Reason { get; set; }
        
        [Column("fee")]
        public Money18 Fee { get; set; }

        [Required]
        [Column("asset_symbol")]
        public string AssetSymbol { get; set; }

        public static FeeCollectedOperationEntity Create(FeeCollectedOperationDto operation)
        {
            return new FeeCollectedOperationEntity
            {
                Id = operation.OperationId,
                Fee = operation.Fee,
                CustomerId = operation.CustomerId,
                Timestamp = operation.Timestamp,
                AssetSymbol = operation.AssetSymbol,
                Reason = operation.Reason,
            };
        }
    }
}
