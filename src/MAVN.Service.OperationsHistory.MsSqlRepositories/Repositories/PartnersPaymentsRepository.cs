using System;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Numerics;
using MAVN.Common.MsSql;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Repositories
{
    public class PartnersPaymentsRepository : IPartnersPaymentsRepository
    {
        private readonly MsSqlContextFactory<OperationsHistoryContext> _contextFactory;

        public PartnersPaymentsRepository(MsSqlContextFactory<OperationsHistoryContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddPartnerPaymentAsync(PartnerPaymentDto partnerPayment)
        {
            var historyEntity = TransactionHistoryEntity.CreateForPartnerPaymentTokensReservation(partnerPayment);
            var partnerPaymentEntity = PartnersPaymentEntity.Create(partnerPayment);

            using (var context = _contextFactory.CreateDataContext())
            {
                context.PartnersPayments.Add(partnerPaymentEntity);
                context.TransactionHistories.Add(historyEntity);

                await context.SaveChangesAsync();
            }
        }

        public async Task AddPartnerPaymentRefundAsync(PartnerPaymentDto partnerPayment)
        {
            var historyEntity = TransactionHistoryEntity.CreateForPartnersPaymentRefund(partnerPayment);
            var partnerPaymentEntity = PartnersPaymentRefundEntity.Create(partnerPayment);

            using (var context = _contextFactory.CreateDataContext())
            {
                context.RefundedPartnersPayments.Add(partnerPaymentEntity);
                context.TransactionHistories.Add(historyEntity);

                await context.SaveChangesAsync();
            }
        }

        public async Task<Money18> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                // Due to limitation with Money18 we cannot perform Sum or SumAsync
                var amounts = await context.PartnersPayments
                    .Where(c => c.Timestamp >= startDate && c.Timestamp < endDate)
                    .Select(c => c.Amount)
                    .ToListAsync();

                Money18 sum = 0;
                amounts.ForEach(c => sum += c);

                return sum;
            }
        }

        public async Task<Money18> GetRefundedTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                // Due to limitation with Money18 we cannot perform Sum or SumAsync
                var amounts = await context.RefundedPartnersPayments
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
                var amounts = await context.PartnersPayments
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

        public async Task<Money18> GetRefundedTotalAmountForCustomerAndPeriodAsync(string customerId, DateTime? startDate, DateTime? endDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                // Due to limitation with Money18 we cannot perform Sum or SumAsync
                var amounts = await context.RefundedPartnersPayments
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

        public async Task<PaginatedPartnersPaymentsHistory> GetByDatesPaginatedAsync(DateTime dateFrom, DateTime dateTo, int skip, int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var payments = context.PartnersPayments.Where(p =>
                    p.Timestamp >= dateFrom && p.Timestamp < dateTo
                    //Need to exclude refunded payments for integration with accounting system
                    && context.RefundedPartnersPayments.All(r => r.PaymentRequestId != p.PaymentRequestId));

                var totalCount = await payments.CountAsync();

                var result = await payments
                    .OrderBy(t => t.Timestamp)
                    .Skip(skip)
                    .Take(take)
                    .ToArrayAsync();

                return new PaginatedPartnersPaymentsHistory
                {
                    PartnersPaymentsHistory = result,
                    TotalCount = totalCount
                };
            }
        }
    }
}
