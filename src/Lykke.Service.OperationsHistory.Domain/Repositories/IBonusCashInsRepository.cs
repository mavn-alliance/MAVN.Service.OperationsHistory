using System;
using System.Threading.Tasks;
using Falcon.Numerics;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.Domain.Repositories
{
    public interface IBonusCashInsRepository
    {
        Task AddAsync(BonusCashInDto bonusCashIn);

        Task<Money18> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);

        Task<Money18> GetTotalAmountForCustomerAndPeriodAsync(string customerId, DateTime? startDate, DateTime? endDate);

        Task<PaginatedBonusesHistory> GetByDatesPaginatedAsync(
            DateTime dateFrom,
            DateTime dateTo,
            int skip,
            int take);
    }
}
