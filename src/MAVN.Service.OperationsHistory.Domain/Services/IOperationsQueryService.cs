using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.Domain.Services
{
    public interface IOperationsQueryService
    {
        Task<PaginatedCustomerOperationsModel> GetByCustomerIdPaginatedAsync
            (string customerId, int currentPage, int pageSize);

        Task<PaginatedTransactionHistory> GetByDatePaginatedAsync
            (DateTime fromDate, DateTime toDate, int currentPage, int pageSize);

        Task<PaginatedBonusesHistory> GetBonusesByDatesPaginatedAsync(
            DateTime fromDate,
            DateTime toDate,
            int currentPage,
            int pageSize);

        Task<PaginatedPartnersPaymentsHistory> GetPartnersPaymentsByDatesPaginatedAsync(
            DateTime fromDate,
            DateTime toDate,
            int currentPage,
            int pageSize);

        Task<PaginatedVoucherPurchasePaymentsHistory> GetVoucherPurchasePaymentsByDatesPaginatedAsync(
            DateTime fromDate,
            DateTime toDate,
            int currentPage,
            int pageSize);

        Task<PaginatedSmartVoucherPaymentsHistory> GetSmartVoucherPaymentsByDatesPaginatedAsync(
            DateTime fromDate,
            DateTime toDate,
            int currentPage,
            int pageSize);

        Task<IEnumerable<IBonusCashIn>> GetBonusCashInsAsync(string customerId, string campaignId);

        Task<IEnumerable<IBonusCashIn>> GetBonusCashInsByReferralAsync(string customerId, string referralId);
    }
}
