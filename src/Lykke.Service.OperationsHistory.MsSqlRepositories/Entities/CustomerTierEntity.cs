using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("tiers_updates")]
    public class CustomerTierEntity
    {
        public CustomerTierEntity()
        {
        }

        public CustomerTierEntity(CustomerTierModel model)
        {
            Id = model.Id;
            CustomerId = model.CustomerId;
            TierId = model.TierId;
            Timestamp = model.Timestamp;
        }

        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Required]
        [Column("customer_id", TypeName = "char(36)")]
        public string CustomerId { get; set; }

        [Required]
        [Column("tier_id", TypeName = "char(36)")]
        public string TierId { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
