using MAVN.Service.OperationsHistory.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAVN.Service.OperationsHistory.Domain.Repositories
{
    public interface ITransactionHistoryRepository
    {
        Task<PaginatedCustomerOperationsModel> GetByCustomerIdPaginatedAsync(string customerId, int skip, int take);

        Task<PaginatedTransactionHistory> GetByDatePaginatedAsync(DateTime dateFrom, DateTime dateTo, int skip, int take);

        Task<int> GetActiveCustomersCountAsync(DateTime dateFrom, DateTime dateTo);

        Task<CustomersStatisticListModel> GetActiveCustomersStatisticAsync(DateTime fromDate, DateTime toDate);

        Task<IEnumerable<IBonusCashIn>> GetBonusCashInsAsync(string customerId, string campaignId);

        Task<IEnumerable<IBonusCashIn>> GetBonusCashInsByReferralAsync(string customerId, string referralId);
    }
}
