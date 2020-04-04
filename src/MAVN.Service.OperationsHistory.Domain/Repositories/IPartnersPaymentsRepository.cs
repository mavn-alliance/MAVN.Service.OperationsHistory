using System;
using System.Threading.Tasks;
using Falcon.Numerics;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.Domain.Repositories
{
    public interface IPartnersPaymentsRepository
    {
        Task AddPartnerPaymentAsync(PartnerPaymentDto partnerPayment);

        Task AddPartnerPaymentRefundAsync(PartnerPaymentDto partnerPayment);

        Task<Money18> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);

        Task<Money18> GetRefundedTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);

        Task<Money18> GetTotalAmountForCustomerAndPeriodAsync(string customerId, DateTime? startDate, DateTime? endDate);

        Task<Money18> GetRefundedTotalAmountForCustomerAndPeriodAsync(string customerId, DateTime? startDate, DateTime? endDate);

        Task<PaginatedPartnersPaymentsHistory> GetByDatesPaginatedAsync(
            DateTime dateFrom,
            DateTime dateTo,
            int skip,
            int take);
    }
}
