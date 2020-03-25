using System;
using System.Threading.Tasks;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.Domain.Repositories
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
