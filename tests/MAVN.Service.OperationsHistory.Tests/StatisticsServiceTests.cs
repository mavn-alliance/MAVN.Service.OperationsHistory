using System;
using System.Threading.Tasks;
using Falcon.Numerics;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.DomainServices.Services;
using Moq;
using Xunit;

namespace MAVN.Service.OperationsHistory.Tests
{
    public class StatisticsServiceTests
    {
        private const string FakeCustomerId = "custId";
        private const string TokenSymbol = "MVN";
        private static readonly Money18 TotalBonusesAmount = 100;
        private static readonly Money18 TotalPartnersPaymentsAmount = 120;
        private static readonly Money18 RefundedPartnersPaymentsAmount = 65;

        private readonly Mock<ITransactionHistoryRepository> _transactionHistoryRepoMock = new Mock<ITransactionHistoryRepository>();
        private readonly Mock<IBonusCashInsRepository> _bonusRepoMock = new Mock<IBonusCashInsRepository>();
        private readonly Mock<IPartnersPaymentsRepository> _partnersPaymentsRepoMock = new Mock<IPartnersPaymentsRepository>();

        [Fact]
        public async Task GetTokensStatisticsAsync_CalculatedEarnedAndBurnedAmount()
        {
            _bonusRepoMock.Setup(x => x.GetTotalAmountByPeriodAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(TotalBonusesAmount);

            _partnersPaymentsRepoMock
                .Setup(x => x.GetTotalAmountByPeriodAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(TotalPartnersPaymentsAmount);

            _partnersPaymentsRepoMock.Setup(x =>
                    x.GetRefundedTotalAmountByPeriodAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(RefundedPartnersPaymentsAmount);

            var sut = CreateSutInstance();

            var expectedTotalBurned = 55;

            var result = await sut.GetTokensStatisticsAsync(DateTime.UtcNow, DateTime.UtcNow.AddDays(1));

            Assert.Equal(expectedTotalBurned, result[0].BurnedAmount);
            Assert.Equal(TotalBonusesAmount, result[0].EarnedAmount);
        }

        [Fact]
        public async Task GetTokensStatisticsForCustomerAsync_CalculatedEarnedAndBurnedAmount()
        {
            _bonusRepoMock.Setup(x =>
                    x.GetTotalAmountForCustomerAndPeriodAsync(FakeCustomerId, It.IsAny<DateTime>(),
                        It.IsAny<DateTime>()))
                .ReturnsAsync(TotalBonusesAmount);

            _partnersPaymentsRepoMock
                .Setup(x => x.GetTotalAmountForCustomerAndPeriodAsync(FakeCustomerId, It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(TotalPartnersPaymentsAmount);

            _partnersPaymentsRepoMock.Setup(x =>
                    x.GetRefundedTotalAmountForCustomerAndPeriodAsync(FakeCustomerId, It.IsAny<DateTime>(),
                        It.IsAny<DateTime>()))
                .ReturnsAsync(RefundedPartnersPaymentsAmount);

            var sut = CreateSutInstance();

            var expectedTotalBurned = 55;

            var result =
                await sut.GetTokensStatisticsForCustomerAsync(FakeCustomerId, DateTime.UtcNow,
                    DateTime.UtcNow.AddDays(1));


            Assert.Equal(expectedTotalBurned, result.BurnedAmount);
            Assert.Equal(TotalBonusesAmount, result.EarnedAmount);
        }

        public StatisticsService CreateSutInstance()
        {
            return new StatisticsService(
                _transactionHistoryRepoMock.Object,
                _bonusRepoMock.Object,
                _partnersPaymentsRepoMock.Object,
                TokenSymbol);
        }
    }
}
