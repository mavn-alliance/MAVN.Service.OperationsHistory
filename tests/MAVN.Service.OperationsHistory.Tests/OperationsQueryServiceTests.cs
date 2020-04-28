using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Logs;
using Lykke.Service.CustomerProfile.Client;
using Lykke.Service.CustomerProfile.Client.Models.Responses;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.DomainServices.Services;
using Moq;
using Xunit;

namespace MAVN.Service.OperationsHistory.Tests
{
    public class OperationsQueryServiceTests
    {
        private const string FakeSenderCustomerId = "senderId";
        private const string FakeReceiverCustomerId = "receiverId";
        private const string FakeReceiver2CustomerId = "receiver2Id";
        private const string FakeSenderCustomerEmail = "senderEmail";
        private const string FakeReceiverCustomerEmail = "receiverEmail";
        private const string FakeReceiver2CustomerEmail = "receiver2Email";

        private readonly Mock<ITransactionHistoryRepository> _transactionsHistoryRepoMock = new Mock<ITransactionHistoryRepository>();
        private readonly Mock<IBonusCashInsRepository> _bonusesRepoMock = new Mock<IBonusCashInsRepository>();
        private readonly Mock<IPaymentTransfersRepository> _paymentTransfersRepoMock = new Mock<IPaymentTransfersRepository>();
        private readonly Mock<IPartnersPaymentsRepository> _partnersPaymentsRepoMock = new Mock<IPartnersPaymentsRepository>();
        private readonly Mock<IVoucherPurchasePaymentsRepository> _vouchersPaymentsRepoMock = new Mock<IVoucherPurchasePaymentsRepository>();
        private readonly Mock<ISmartVoucherPaymentsRepository> _smartVoucherPaymentsRepoMock = new Mock<ISmartVoucherPaymentsRepository>();
        private readonly Mock<ICustomerProfileClient> _cPClientMock = new Mock<ICustomerProfileClient>();

        [Fact]
        public async Task GetByCustomerIdPaginatedAsync_CustomersEmailsArePopulated()
        {
            _transactionsHistoryRepoMock.Setup(x =>
                    x.GetByCustomerIdPaginatedAsync(FakeSenderCustomerId, It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new PaginatedCustomerOperationsModel
                {
                    Transfers = new List<Transfer>
                    {
                        new Transfer
                        {
                            SenderCustomerId = FakeSenderCustomerId,
                            ReceiverCustomerId = FakeReceiverCustomerId
                        },
                        new Transfer
                        {
                            SenderCustomerId = FakeSenderCustomerId,
                            ReceiverCustomerId = FakeReceiver2CustomerId
                        }
                    }
                });

            var customerIds = new [] {FakeSenderCustomerId, FakeReceiverCustomerId, FakeReceiver2CustomerId};
            var customerProfiles = new List<CustomerProfile>
            {
                new CustomerProfile
                {
                    CustomerId = FakeSenderCustomerId,
                    Email = FakeSenderCustomerEmail
                },
                new CustomerProfile
                {
                    CustomerId = FakeReceiverCustomerId,
                    Email = FakeReceiverCustomerEmail
                },
                new CustomerProfile
                {
                    CustomerId = FakeReceiver2CustomerId,
                    Email = FakeReceiver2CustomerEmail
                },
            };

            _cPClientMock.Setup(x => x.CustomerProfiles.GetByIdsAsync(customerIds, It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(customerProfiles);

            var sut = CreateSutInstance();

            var result = await sut.GetByCustomerIdPaginatedAsync(FakeSenderCustomerId, 1, 10);

            var fakeReceiverTransfer = result.Transfers.First(t => t.ReceiverCustomerId == FakeReceiverCustomerId);
            var fakeReceiver2Transfer = result.Transfers.First(t => t.ReceiverCustomerId == FakeReceiver2CustomerId);

            Assert.Equal(fakeReceiverTransfer.ReceiverCustomerEmail, FakeReceiverCustomerEmail);
            Assert.Equal(fakeReceiverTransfer.SenderCustomerEmail, FakeSenderCustomerEmail);
            Assert.Equal(fakeReceiver2Transfer.ReceiverCustomerEmail, FakeReceiver2CustomerEmail);
            Assert.Equal(fakeReceiver2Transfer.SenderCustomerEmail, FakeSenderCustomerEmail);
        }

        public OperationsQueryService CreateSutInstance()
        {
            return new OperationsQueryService(
                _transactionsHistoryRepoMock.Object,
                _bonusesRepoMock.Object,
                _paymentTransfersRepoMock.Object,
                _partnersPaymentsRepoMock.Object,
                _vouchersPaymentsRepoMock.Object,
                _cPClientMock.Object,
                _smartVoucherPaymentsRepoMock.Object,
                EmptyLogFactory.Instance);
        }
    }
}
