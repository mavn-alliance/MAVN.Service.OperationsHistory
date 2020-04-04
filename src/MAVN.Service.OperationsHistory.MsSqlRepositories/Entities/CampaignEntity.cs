using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Entities
{
    [Table("campaigns")]
    public class CampaignEntity
    {
        [Key]
        [Required]
        public string CampaignId { get; set; }

        [Required]
        public string CampaignName { get; set; }

        public static CampaignEntity Create(string campaignId, string campaignName)
        {
            return new CampaignEntity
            {
                CampaignId = campaignId,
                CampaignName = campaignName
            };
        }
    }
}
