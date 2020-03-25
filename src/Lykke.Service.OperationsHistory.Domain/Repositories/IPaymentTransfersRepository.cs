using System;
using System.Threading.Tasks;
using Falcon.Numerics;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.Domain.Repositories
{
    public interface IPaymentTransfersRepository
    {
        Task AddPaymentTransferAsync(PaymentTransferDto paymentTransfer);
        Task<Money18> GetTotalAmountForCustomerAndPeriodAsync(string customerId, DateTime? startDate, DateTime? endDate);
        Task AddPaymentTransferRefundAsync(PaymentTransferDto paymentTransfer);
        Task<Money18> GetTotalAmountByPeriodAsync(DateTime startDate, DateTime endDate);
        Task<Money18> GetRefundedTotalAmountForCustomerAndPeriodAsync(string customerId, DateTime? startDate,
            DateTime? endDate);
        Task<Money18> GetRefundedTotalAmountByPeriodAsync(DateTime startDate,
            DateTime endDate);
        Task<PaginatedPaymentTransfersHistory> GetByDatesPaginatedAsync(
            DateTime dateFrom,
            DateTime dateTo,
            int skip,
            int take);
    }
}
