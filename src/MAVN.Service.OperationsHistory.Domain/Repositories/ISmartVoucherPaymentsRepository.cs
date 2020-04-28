using System;
using System.Threading.Tasks;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.Domain.Repositories
{
    public interface ISmartVoucherPaymentsRepository
    {
        Task AddAsync(ISmartVoucherPayment payment);

        Task<PaginatedSmartVoucherPaymentsHistory> GetByDatesPaginatedAsync(
            DateTime dateFrom,
            DateTime dateTo,
            int skip,
            int take);
    }
}
