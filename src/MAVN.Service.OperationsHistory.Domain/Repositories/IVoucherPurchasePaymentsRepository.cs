using System;
using System.Threading.Tasks;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.Domain.Repositories
{
    public interface IVoucherPurchasePaymentsRepository
    {
        Task InsertAsync(VoucherPurchasePaymentDto voucherPurchasePaymentOperation);

        Task<PaginatedVoucherPurchasePaymentsHistory> GetByDatesPaginatedAsync(
            DateTime dateFrom,
            DateTime dateTo,
            int skip,
            int take);
    }
}
