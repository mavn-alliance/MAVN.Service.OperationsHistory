using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.Domain.Services
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

        Task<PaginatedPaymentTransfersHistory> GetPaymentTransfersByDatesPaginatedAsync(
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

        Task<IEnumerable<IBonusCashIn>> GetBonusCashInsAsync(string customerId, string campaignId);

        Task<IEnumerable<IBonusCashIn>> GetBonusCashInsByReferralAsync(string customerId, string referralId);
    }
}
