using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Falcon.Numerics;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.Domain.Services;

namespace MAVN.Service.OperationsHistory.DomainServices.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        private readonly IBonusCashInsRepository _bonusCashInsRepository;
        private readonly IPaymentTransfersRepository _paymentTransfersRepository;
        private readonly IPartnersPaymentsRepository _partnersPaymentsRepository;
        private readonly string _tokenSymbol;

        public StatisticsService(
            ITransactionHistoryRepository transactionHistoryRepository,
            IBonusCashInsRepository bonusCashInsRepository,
            IPaymentTransfersRepository paymentTransfersRepository,
            IPartnersPaymentsRepository partnersPaymentsRepository,
            string tokenSymbol)
        {
            _transactionHistoryRepository = transactionHistoryRepository;
            _bonusCashInsRepository = bonusCashInsRepository;
            _paymentTransfersRepository = paymentTransfersRepository;
            _partnersPaymentsRepository = partnersPaymentsRepository;
            _tokenSymbol = tokenSymbol;
        }

        public Task<int> GetActiveCustomersCountAsync(DateTime dateFrom, DateTime dateTo)
        {
            return _transactionHistoryRepository.GetActiveCustomersCountAsync(dateFrom, dateTo);
        }

        public async Task<IReadOnlyList<TokensAmountResultModel>> GetTokensStatisticsAsync(
            DateTime dateFrom,
            DateTime dateTo)
        {
            var bonusesAmount = await _bonusCashInsRepository.GetTotalAmountByPeriodAsync(dateFrom, dateTo);
            var totalPaymentTransferAmount =
                await _paymentTransfersRepository.GetTotalAmountByPeriodAsync(dateFrom, dateTo);
            var totalRefundedPaymentTransferAmount =
                await _paymentTransfersRepository.GetRefundedTotalAmountByPeriodAsync(dateFrom, dateTo);
            var totalPartnersPaymentsAmount =
                await _partnersPaymentsRepository.GetTotalAmountByPeriodAsync(dateFrom, dateTo);
            var totalRefundedPartnersPaymentsAmount =
                await _partnersPaymentsRepository.GetRefundedTotalAmountByPeriodAsync(dateFrom, dateTo);

            var totalBurned = CalculateTotalBurned(totalPaymentTransferAmount, totalRefundedPaymentTransferAmount,
                totalPartnersPaymentsAmount, totalRefundedPartnersPaymentsAmount);

            var result = new TokensAmountResultModel
            {
                Asset = _tokenSymbol,
                EarnedAmount = bonusesAmount,
                BurnedAmount = totalBurned
            };

            return new List<TokensAmountResultModel> { result };
        }

        public async Task<TokensAmountResultModel> GetTokensStatisticsForCustomerAsync(
            string customerId,
            DateTime? startDate,
            DateTime? endDate)
        {
            var totalBonusesAmount =
                await _bonusCashInsRepository.GetTotalAmountForCustomerAndPeriodAsync(customerId, startDate, endDate);
            var totalPaymentTransferAmount =
                await _paymentTransfersRepository.GetTotalAmountForCustomerAndPeriodAsync(customerId, startDate,
                    endDate);
            var totalRefundedPaymentTransferAmount =
                await _paymentTransfersRepository.GetRefundedTotalAmountForCustomerAndPeriodAsync(customerId, startDate,
                    endDate);
            var totalPartnersPaymentsAmount =
                await _partnersPaymentsRepository.GetTotalAmountForCustomerAndPeriodAsync(customerId, startDate,
                    endDate);
            var totalRefundedPartnersPaymentsAmount =
                await _partnersPaymentsRepository.GetRefundedTotalAmountForCustomerAndPeriodAsync(customerId, startDate,
                    endDate);

            var totalBurnedAmount = CalculateTotalBurned(totalPaymentTransferAmount, totalRefundedPaymentTransferAmount,
                totalPartnersPaymentsAmount, totalRefundedPartnersPaymentsAmount);

            var result = new TokensAmountResultModel
            {
                Asset = _tokenSymbol,
                EarnedAmount = totalBonusesAmount,
                BurnedAmount = totalBurnedAmount
            };

            return result;
        }

        private Money18 CalculateTotalBurned(
            Money18 totalPaymentTransfersAmount,
            Money18 totalRefundedPaymentTransfersAmount,
            Money18 totalPartnersPaymentsAmount,
            Money18 totalRefundedPartnersPaymentsAmount)
        {
            var total = totalPartnersPaymentsAmount + totalPaymentTransfersAmount -
                        totalRefundedPartnersPaymentsAmount - totalRefundedPaymentTransfersAmount;

            return total;
        }

        public async Task<CustomersStatisticListModel> GetCustomersStatisticsByDayAsync(DateTime fromDate, DateTime toDate)
        {
            return await _transactionHistoryRepository.GetActiveCustomersStatisticAsync(fromDate, toDate);
        }
    }
}
