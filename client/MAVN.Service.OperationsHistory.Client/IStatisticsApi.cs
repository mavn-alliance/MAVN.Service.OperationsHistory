using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MAVN.Service.OperationsHistory.Client.Models.Requests;
using MAVN.Service.OperationsHistory.Client.Models.Responses;
using Refit;

namespace MAVN.Service.OperationsHistory.Client
{
    /// <summary>
    /// Statistics client API interface.
    /// </summary>
    [PublicAPI]
    public interface IStatisticsApi
    {
        /// <summary>
        /// Gets the count of active customers between two dates
        /// </summary>
        /// <returns><see cref="ActiveCustomersCountResponse"/></returns>
        [Get("/api/statistics/customers/active")]
        Task<ActiveCustomersCountResponse> GetActiveCustomersCountAsync(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Get the tokens statistics between two dates
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        [Get("/api/statistics/tokens")]
        Task<IEnumerable<TokensAmountResponseModel>> GetTokensStatisticsAsync(DateTime dateFrom, DateTime dateTo);

        /// <summary>
        /// Get the customers statistics
        /// </summary>
        /// <param name="periodRequest"><see cref="PeriodRequest"/></param>
        /// <returns><see cref="CustomersStatisticListResponse"/></returns>
        [Get("/api/statistics/customers")]
        Task<CustomersStatisticListResponse> GetCustomerStatisticsAsync(PeriodRequest periodRequest);

        /// <summary>
        /// Gets statistics for tokens for a customer in the system
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Get("/api/statistics/customers/tokens")]
        Task<TokensAmountResponseModel> GetTokensStatisticsForCustomerAsync(TokensStatisticsForCustomerRequest request);
    }
}
