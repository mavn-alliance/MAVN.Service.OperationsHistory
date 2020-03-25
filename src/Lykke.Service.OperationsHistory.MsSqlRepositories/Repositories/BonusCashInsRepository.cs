using System;
using System.Linq;
using System.Threading.Tasks;
using Falcon.Numerics;
using Lykke.Common.MsSql;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.Domain.Repositories;
using Lykke.Service.OperationsHistory.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Repositories
{
    public class BonusCashInsRepository : IBonusCashInsRepository
    {
        private readonly MsSqlContextFactory<OperationsHistoryContext> _contextFactory;

        public BonusCashInsRepository(MsSqlContextFactory<OperationsHistoryContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddAsync(BonusCashInDto bonusCashIn)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var historyEntity = TransactionHistoryEntity.Create(bonusCashIn);
                var bonusEntity = BonusCashInEntity.Create(bonusCashIn);

                var campaign = await context.Campaigns.FindAsync(bonusCashIn.CampaignId);

                if (campaign != null && campaign.CampaignName != bonusCashIn.CampaignName)
                    campaign.CampaignName = bonusCashIn.CampaignName;

                if (campaign == null)
                    campaign = CampaignEntity.Create(bonusCashIn.CampaignId, bonusCashIn.CampaignName);

                bonusEntity.Campaign = campaign;

                context.BonusCashIns.Add(bonusEntity);
                context.TransactionHistories.Add(historyEntity);

                await context.SaveChangesAsync();
            }
        }

        public async Task<Money18> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                // Due to limitation with Money18 we cannot perform Sum or SumAsync
                var amounts = await context.BonusCashIns
                    .Where(c => c.Timestamp >= startDate && c.Timestamp < endDate)
                    .Select(c => c.Amount)
                    .ToListAsync();

                Money18 sum = 0;
                amounts.ForEach(c => sum += c);

                return sum;
            }
        }

        public async Task<Money18> GetTotalAmountForCustomerAndPeriodAsync(string customerId, DateTime? startDate, DateTime? endDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                // Due to limitation with Money18 we cannot perform Sum or SumAsync
                var amounts = await context.BonusCashIns
                    .Where(p => p.CustomerId == customerId &&
                                (startDate == null || p.Timestamp >= startDate.Value.Date) &&
                                (endDate == null || p.Timestamp <= endDate.Value.Date))
                    .Select(c => c.Amount)
                    .ToListAsync();

                Money18 sum = 0;
                amounts.ForEach(c => sum += c);

                return sum;
            }
        }

        public async Task<PaginatedBonusesHistory> GetByDatesPaginatedAsync(
            DateTime dateFrom,
            DateTime dateTo,
            int skip,
            int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var bonuses = context.BonusCashIns
                    .Where(t => t.Timestamp >= dateFrom && t.Timestamp < dateTo);

                var totalCount = await bonuses.CountAsync();

                var result = await bonuses
                    .OrderBy(t => t.Timestamp)
                    .Skip(skip)
                    .Take(take)
                    .ToArrayAsync();

                return new PaginatedBonusesHistory
                {
                    BonusesHistory = result,
                    TotalCount = totalCount
                };
            }
        }
    }
}
