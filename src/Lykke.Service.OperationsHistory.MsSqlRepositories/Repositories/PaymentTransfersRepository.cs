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
    public class PaymentTransfersRepository : IPaymentTransfersRepository
    {
        private readonly MsSqlContextFactory<OperationsHistoryContext> _contextFactory;

        public PaymentTransfersRepository(MsSqlContextFactory<OperationsHistoryContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddPaymentTransferAsync(PaymentTransferDto paymentTransfer)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var historyEntity = TransactionHistoryEntity.CreateForPaymentTransferTokensReservation(paymentTransfer);
                var transferEntity = PaymentTransferEntity.Create(paymentTransfer);

                var burnRule =
                    await context.BurnRules.FindAsync(paymentTransfer.BurnRuleId);

                if (burnRule != null && burnRule.BurnRuleName != paymentTransfer.BurnRuleName)
                    burnRule.BurnRuleName = paymentTransfer.BurnRuleName;

                if (burnRule == null)
                    burnRule = BurnRuleEntity.Create(paymentTransfer.BurnRuleId,paymentTransfer.BurnRuleName);

                transferEntity.BurnRule = burnRule;

                context.PaymentTransfers.Add(transferEntity);
                context.TransactionHistories.Add(historyEntity);

                await context.SaveChangesAsync();
            }
        }

        public async Task AddPaymentTransferRefundAsync(PaymentTransferDto paymentTransfer)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var historyEntity = TransactionHistoryEntity.CreateForPaymentTransferRefund(paymentTransfer);
                var transferEntity = PaymentTransferRefundEntity.Create(paymentTransfer);

                var burnRule =
                    await context.BurnRules.FindAsync(paymentTransfer.BurnRuleId);

                if (burnRule != null && burnRule.BurnRuleName != paymentTransfer.BurnRuleName)
                    burnRule.BurnRuleName = paymentTransfer.BurnRuleName;

                if (burnRule == null)
                    burnRule = BurnRuleEntity.Create(paymentTransfer.BurnRuleId, paymentTransfer.BurnRuleName);

                transferEntity.BurnRule = burnRule;

                context.RefundedPaymentTransfers.Add(transferEntity);
                context.TransactionHistories.Add(historyEntity);

                await context.SaveChangesAsync();
            }
        }

        public async Task<Money18> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                // Due to limitation with Money18 we cannot perform Sum or SumAsync
                var amounts = await context.PaymentTransfers
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
                var amounts = await context.RefundedPaymentTransfers
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
                var amounts = await context.PaymentTransfers
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
                var amounts = await context.RefundedPaymentTransfers
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

        public async Task<PaginatedPaymentTransfersHistory> GetByDatesPaginatedAsync(
            DateTime dateFrom,
            DateTime dateTo,
            int skip,
            int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var payments = context.PaymentTransfers.Where(p =>
                    p.Timestamp >= dateFrom && p.Timestamp < dateTo
                    //Need to exclude refunded payments for integration with accounting system
                    && context.RefundedPaymentTransfers.All(r => r.TransferId != p.TransferId));

                var totalCount = await payments.CountAsync();

                var result = await payments
                    .OrderBy(t => t.Timestamp)
                    .Skip(skip)
                    .Take(take)
                    .ToArrayAsync();

                return new PaginatedPaymentTransfersHistory
                {
                    PaymentTransfersHistory = result,
                    TotalCount = totalCount
                };
            }
        }
    }
}
