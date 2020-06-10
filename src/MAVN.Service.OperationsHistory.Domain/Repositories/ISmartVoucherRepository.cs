using System;
using System.Threading.Tasks;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.Domain.Repositories
{
    public interface ISmartVoucherRepository
    {
        Task AddPaymentAsync(SmartVoucherPaymentDto payment);
        Task AddUseAsync(SmartVoucherUseDto smartVoucher);
        Task AddTransferAsync(SmartVoucherTransferDto smartVoucher);

        Task<PaginatedSmartVoucherPaymentsHistory> GetByDatesPaginatedAsync(
            DateTime dateFrom,
            DateTime dateTo,
            int skip,
            int take);
    }
}
