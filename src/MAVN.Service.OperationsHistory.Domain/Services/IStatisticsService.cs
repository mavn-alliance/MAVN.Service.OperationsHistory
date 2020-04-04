using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.Domain.Services
{
    public interface IStatisticsService
    {
        Task<int> GetActiveCustomersCountAsync(DateTime dateFrom, DateTime dateTo);
        Task<IReadOnlyList<TokensAmountResultModel>> GetTokensStatisticsAsync(DateTime dateFrom, DateTime dateTo);
        Task<CustomersStatisticListModel> GetCustomersStatisticsByDayAsync(DateTime fromDate, DateTime toDate);
        Task<TokensAmountResultModel> GetTokensStatisticsForCustomerAsync(string customerId, DateTime? startDate,
            DateTime? endDate);
    }
}
