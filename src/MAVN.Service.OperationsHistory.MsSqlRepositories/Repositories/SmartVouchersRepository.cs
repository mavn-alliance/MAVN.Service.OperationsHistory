using System;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Repositories
{
    public class SmartVouchersRepository : ISmartVoucherRepository
    {
        private readonly PostgreSQLContextFactory<OperationsHistoryContext> _contextFactory;

        public SmartVouchersRepository(PostgreSQLContextFactory<OperationsHistoryContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddPaymentAsync(SmartVoucherPaymentDto payment)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = SmartVoucherPaymentEntity.Create(payment);
                var historyEntity = TransactionHistoryEntity.CreateForSmartVoucherPayment(payment);

                entity.Campaign = await GetAndUpdateCampaign(context, payment.CampaignId, payment.CampaignName);

                context.SmartVoucherPayments.Add(entity);
                context.TransactionHistories.Add(historyEntity);

                await context.SaveChangesAsync();
            }
        }

        public async Task AddUseAsync(SmartVoucherUseDto smartVoucher)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = SmartVoucherUseEntity.Create(smartVoucher);
                var historyEntity = TransactionHistoryEntity.CreateForSmartVoucherUse(smartVoucher);

                entity.Campaign = await GetAndUpdateCampaign(context, smartVoucher.CampaignId, smartVoucher.CampaignName);

                context.SmartVoucherUses.Add(entity);
                context.TransactionHistories.Add(historyEntity);

                await context.SaveChangesAsync();
            }
        }

        public async Task AddTransferAsync(SmartVoucherTransferDto transfer)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = SmartVoucherTransferEntity.Create(transfer);
                var historyEntityForSender = TransactionHistoryEntity.CreateForSmartVoucherTransferSender(transfer);
                var historyEntityForReceiver = TransactionHistoryEntity.CreateForSmartVoucherTransferReceiver(transfer);

                entity.Campaign = await GetAndUpdateCampaign(context, transfer.CampaignId, transfer.CampaignName);

                context.SmartVoucherTransfers.Add(entity);
                context.TransactionHistories.Add(historyEntityForSender);
                context.TransactionHistories.Add(historyEntityForReceiver);

                await context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedSmartVoucherPaymentsHistory> GetByDatesPaginatedAsync(
            DateTime dateFrom,
            DateTime dateTo,
            int skip,
            int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var payments = context.SmartVoucherPayments.Where(p =>
                    p.Timestamp >= dateFrom && p.Timestamp < dateTo);

                var totalCount = await payments.CountAsync();

                var result = await payments
                    .OrderBy(t => t.Timestamp)
                    .Skip(skip)
                    .Take(take)
                    .Select(x => new SmartVoucherPaymentDto
                    {
                        Vertical = x.Vertical,
                        PaymentRequestId = x.PaymentRequestId,
                        PartnerName = x.PartnerName,
                        Timestamp = x.Timestamp,
                        CustomerId = x.CustomerId,
                        AssetSymbol = x.AssetSymbol,
                        PartnerId = x.PartnerId,
                        CampaignId = x.CampaignId,
                        CampaignName = x.Campaign.CampaignName,
                        Amount = x.Amount,
                        ShortCode = x.ShortCode,
                    })
                    .ToArrayAsync();

                return new PaginatedSmartVoucherPaymentsHistory
                {
                    SmartVoucherPaymentsHistory = result,
                    TotalCount = totalCount
                };
            }
        }

        private async Task<CampaignEntity> GetAndUpdateCampaign(OperationsHistoryContext context, string campaignId, string campaignName)
        {
            var campaign = await context.Campaigns.FindAsync(campaignId);

            if (campaign != null && !string.IsNullOrEmpty(campaignName) && campaign.CampaignName != campaignName)
                campaign.CampaignName = campaignName;

            if (campaign == null)
                campaign = CampaignEntity.Create(campaignId, campaignName);

            return campaign;
        }
    }
}
