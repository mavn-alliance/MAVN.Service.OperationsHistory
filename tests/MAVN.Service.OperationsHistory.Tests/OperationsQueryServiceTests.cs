using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lykke.Logs;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.CustomerProfile.Client.Models.Responses;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.DomainServices.Services;
using Moq;
using Xunit;

namespace MAVN.Service.OperationsHistory.Tests
{
    public class OperationsQueryServiceTests
    {
        private const string FakeSenderCustomerId = "0d22dded-89ca-412a-8cdc-bc1dc2e4ecc1";
        private const string FakeReceiverCustomerId = "f7e73d55-ef70-4665-8ddb-d910340758c4";
        private const string FakeReceiver2CustomerId = "8924b275-f634-4ae4-b0a2-d9cbba38e2f4";
        private const string FakeSenderCustomerEmail = "senderEmail";
        private const string FakeReceiverCustomerEmail = "receiverEmail";
        private const string FakeReceiver2CustomerEmail = "receiver2Email";

        private readonly Mock<ITransactionHistoryRepository> _transactionsHistoryRepoMock = new Mock<ITransactionHistoryRepository>();
        private readonly Mock<IBonusCashInsRepository> _bonusesRepoMock = new Mock<IBonusCashInsRepository>();
        private readonly Mock<IPartnersPaymentsRepository> _partnersPaymentsRepoMock = new Mock<IPartnersPaymentsRepository>();
        private readonly Mock<IVoucherPurchasePaymentsRepository> _vouchersPaymentsRepoMock = new Mock<IVoucherPurchasePaymentsRepository>();
        private readonly Mock<ISmartVoucherRepository> _smartVoucherPaymentsRepoMock = new Mock<ISmartVoucherRepository>();
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
                    },
                    SmartVoucherTransfers = new List<SmartVoucherTransferDto>
                    {
                        new SmartVoucherTransferDto
                        {
                            OldCustomerId = Guid.Parse(FakeSenderCustomerId),
                            NewCustomerId = Guid.Parse(FakeReceiverCustomerId)
                        },
                        new SmartVoucherTransferDto
                        {
                            OldCustomerId = Guid.Parse(FakeSenderCustomerId),
                            NewCustomerId = Guid.Parse(FakeReceiver2CustomerId)
                        }
                    }
                });

            var customerIds = new [] {FakeSenderCustomerId, FakeReceiverCustomerId, FakeReceiver2CustomerId};
            var customerProfiles = new List<CustomerProfile.Client.Models.Responses.CustomerProfile>
            {
                new CustomerProfile.Client.Models.Responses.CustomerProfile
                {
                    CustomerId = FakeSenderCustomerId,
                    Email = FakeSenderCustomerEmail
                },
                new CustomerProfile.Client.Models.Responses.CustomerProfile
                {
                    CustomerId = FakeReceiverCustomerId,
                    Email = FakeReceiverCustomerEmail
                },
                new CustomerProfile.Client.Models.Responses.CustomerProfile
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
                _partnersPaymentsRepoMock.Object,
                _vouchersPaymentsRepoMock.Object,
                _cPClientMock.Object,
                _smartVoucherPaymentsRepoMock.Object,
                EmptyLogFactory.Instance);
        }
    }
}
