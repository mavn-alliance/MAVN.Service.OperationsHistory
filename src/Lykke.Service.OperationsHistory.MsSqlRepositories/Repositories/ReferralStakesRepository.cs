using System.Threading.Tasks;
using Lykke.Common.MsSql;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.Domain.Repositories;
using Lykke.Service.OperationsHistory.MsSqlRepositories.Entities;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Repositories
{
    public class ReferralStakesRepository : IReferralStakesRepository
    {
        private readonly MsSqlContextFactory<OperationsHistoryContext> _contextFactory;

        public ReferralStakesRepository(MsSqlContextFactory<OperationsHistoryContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddReferralStakeAsync(ReferralStakeDto referralStake)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var historyEntity = TransactionHistoryEntity.CreateForReferralStakeTokensReservation(referralStake);
                var referralStakeEntity = ReferralStakeEntity.Create(referralStake);

                var campaign = await context.Campaigns.FindAsync(referralStake.CampaignId);

                if (campaign != null && campaign.CampaignName != referralStake.CampaignName)
                    campaign.CampaignName = referralStake.CampaignName;

                if (campaign == null)
                    campaign = CampaignEntity.Create(referralStake.CampaignId, referralStake.CampaignName);

                referralStakeEntity.Campaign = campaign;

                context.ReferralStakes.Add(referralStakeEntity);
                context.TransactionHistories.Add(historyEntity);

                await context.SaveChangesAsync();
            }
        }

        public async Task AddReferralStakeReleasedAsync(ReferralStakeDto referralStake)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var historyEntity = TransactionHistoryEntity.CreateForReferralStakeTokensRelease(referralStake);
                var releasedReferralStakeEntity = ReleasedReferralStakeEntity.Create(referralStake);

                var campaign = await context.Campaigns.FindAsync(referralStake.CampaignId);

                if (campaign != null && campaign.CampaignName != referralStake.CampaignName)
                    campaign.CampaignName = referralStake.CampaignName;

                if (campaign == null)
                    campaign = CampaignEntity.Create(referralStake.CampaignId, referralStake.CampaignName);

                releasedReferralStakeEntity.Campaign = campaign;

                context.ReleasedReferralStakes.Add(releasedReferralStakeEntity);
                context.TransactionHistories.Add(historyEntity);

                await context.SaveChangesAsync();
            }
        }
    }
}
