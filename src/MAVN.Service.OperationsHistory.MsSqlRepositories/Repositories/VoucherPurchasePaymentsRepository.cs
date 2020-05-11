using System;
using System.Linq;
using System.Threading.Tasks;
using MAVN.Common.MsSql;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Repositories
{
    public class VoucherPurchasePaymentsRepository : IVoucherPurchasePaymentsRepository
    {
        private readonly MsSqlContextFactory<OperationsHistoryContext> _contextFactory;

        public VoucherPurchasePaymentsRepository(MsSqlContextFactory<OperationsHistoryContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        
        public async Task InsertAsync(VoucherPurchasePaymentDto voucherPurchasePaymentOperation)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var historyEntity = TransactionHistoryEntity.CreateForVoucherTokenReservation(voucherPurchasePaymentOperation);
                var voucherPurchasePaymentEntity = new VoucherPurchasePaymentEntity(voucherPurchasePaymentOperation);

                context.VoucherPurchasePayments.Add(voucherPurchasePaymentEntity);
                context.TransactionHistories.Add(historyEntity);

                await context.SaveChangesAsync();
            }
        }

        public async Task<PaginatedVoucherPurchasePaymentsHistory> GetByDatesPaginatedAsync(
            DateTime dateFrom,
            DateTime dateTo,
            int skip,
            int take)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var payments = context.VoucherPurchasePayments.Where(p =>
                    p.Timestamp >= dateFrom && p.Timestamp < dateTo);

                var totalCount = await payments.CountAsync();

                var result = await payments
                    .OrderBy(t => t.Timestamp)
                    .Skip(skip)
                    .Take(take)
                    .ToArrayAsync();

                return new PaginatedVoucherPurchasePaymentsHistory
                {
                    VoucherPurchasePaymentsHistory = result,
                    TotalCount = totalCount
                };
            }
        }
    }
}
