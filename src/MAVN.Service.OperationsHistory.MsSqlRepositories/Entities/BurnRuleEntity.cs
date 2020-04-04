using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("burn_rules")]
    public class BurnRuleEntity
    {
        [Key]
        [Required]
        [Column("burn_rule_id")]
        public string BurnRuleId { get; set; }

        [Required]
        [Column("burn_rule_name")]
        public string BurnRuleName { get; set; }

        public static BurnRuleEntity Create(string id, string name)
        {
            return new BurnRuleEntity
            {
                BurnRuleId = id,
                BurnRuleName = name
            };
        }
    }
}
