using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lykke.Logs;
using MAVN.Service.Campaign.Client;
using MAVN.Service.Campaign.Client.Models.BurnRule.Responses;
using MAVN.Service.Campaign.Client.Models.Campaign.Responses;
using MAVN.Service.Campaign.Client.Models.Condition;
using MAVN.Service.Campaign.Client.Models.Enums;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.DomainServices.Services;
using MAVN.Service.PartnerManagement.Client;
using MAVN.Service.PrivateBlockchainFacade.Client;
using MAVN.Service.PrivateBlockchainFacade.Client.Models;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace MAVN.Service.OperationsHistory.Tests
{
    public class OperationsServiceTests
    {
        #region Consts

        // ReSharper disable InconsistentNaming
        private const string FakeCustomerWalletAddress = "0x7d275eb17ceaae04b17768d4459741bae062ee09";
        private const string FakePublicWalletAddress = "0xzG375eb17ceaae04b17768d4459741bae062ee09";
        private const string FakeSenderCustomerId = "3f1443e2-b848-4567-8fb5-ebe7337a87e9";
        private const string FakeCampaignId = "b462e17d-24b5-4bb3-8d1a-6963c2610b7c";
        private const string FakeRecipientCustomerId = "60d4f58d-7fde-4640-8d0e-feb97b12a90e";
        private const string FakeConditionId = "21cb616c-b9a9-413c-bdbd-8833428fff1c";
        private const string FakeReferralId = "31cb616c-b9a9-413c-bdbd-8833428fff1c";
        private const string FakeOperationId = "45ab616c-b9a9-413c-bdbd-8833428fff1c";
        private const string FakeConditionName = "condition";
        private const string TokenSymbol = "MVN";
        private const long FakeWalletLinkFee = 1;
        // ReSharper restore InconsistentNaming

        #endregion

        #region Mocks

        private readonly Mock<IBonusCashInsRepository> _bonusRepoMock = new Mock<IBonusCashInsRepository>();
        private readonly Mock<ICampaignClient> _campaignClientMock = new Mock<ICampaignClient>();
        private readonly Mock<ITransfersRepository> _transfersRepoMock = new Mock<ITransfersRepository>();
        private readonly Mock<IPrivateBlockchainFacadeClient> _pbfServiceClientMock = new Mock<IPrivateBlockchainFacadeClient>();
        private readonly Mock<ICustomerTierRepository> _customerTierRepositoryMock = new Mock<ICustomerTierRepository>();
        private readonly Mock<IPartnersPaymentsRepository> _partnersPaymentsRepoMock = new Mock<IPartnersPaymentsRepository>();
        private readonly Mock<IPartnerManagementClient> _partnerManagementClientMock = new Mock<IPartnerManagementClient>();
        private readonly Mock<IReferralStakesRepository> _referralStakesRepoMock = new Mock<IReferralStakesRepository>();
        private readonly Mock<ILinkedWalletTransfersRepository> _linkedWalletTransfersRepositoryMock = new Mock<ILinkedWalletTransfersRepository>();
        private readonly Mock<IFeeCollectedOperationsRepository> _feeCollectedOperationsRepo = new Mock<IFeeCollectedOperationsRepository>();
        private readonly Mock<ILinkWalletOperationsRepository> _linkWalletOperationsRepoMock = new Mock<ILinkWalletOperationsRepository>();
        private readonly Mock<IVoucherPurchasePaymentsRepository> _voucherPurchasePaymentsRepoMock = new Mock<IVoucherPurchasePaymentsRepository>();
        private readonly Mock<ISmartVoucherRepository> _smartVoucherPaymentsRepoMock = new Mock<ISmartVoucherRepository>();

        #endregion

        [Fact]
        public async Task TryToProcessTransferEvent_EverythingValid_SuccessfullyProcessed()
        {
            _transfersRepoMock.Setup(x => x.AddAsync(It.IsAny<ITransfer>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _pbfServiceClientMock
                .Setup(x => x.CustomersApi.GetWalletAddress(It.IsAny<Guid>()))
                .ReturnsAsync(new CustomerWalletAddressResponseModel
                {
                    Error = CustomerWalletAddressError.None,
                    WalletAddress = FakeCustomerWalletAddress
                });

            var transfer = new Transfer
            {
                AssetSymbol = "asset",
                TransactionId = "transactionId",
                Amount = 100,
                SenderCustomerId = FakeSenderCustomerId,
                ReceiverCustomerId = FakeRecipientCustomerId
            };

            var operationsService = CreateSutInstance();

            await operationsService.ProcessTransferEventAsync(transfer);
            _transfersRepoMock.Verify();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10_000_000_000)]
        public async Task TryToProcessTransferEvent_InvalidAmount_SuccessfullyProcessedWithWarning(long amount)
        {
            _transfersRepoMock.Setup(x => x.AddAsync(It.IsAny<ITransfer>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            _pbfServiceClientMock
                .Setup(x => x.CustomersApi.GetWalletAddress(It.IsAny<Guid>()))
                .ReturnsAsync(new CustomerWalletAddressResponseModel
                {
                    Error = CustomerWalletAddressError.None,
                    WalletAddress = FakeCustomerWalletAddress
                });

            var transfer = new Transfer
            {
                AssetSymbol = "asset",
                TransactionId = "transactionId",
                Amount = amount,
                SenderCustomerId = FakeSenderCustomerId,
                ReceiverCustomerId = FakeRecipientCustomerId
            };

            var operationsService = CreateSutInstance();

            await operationsService.ProcessTransferEventAsync(transfer);
            _transfersRepoMock.Verify();
        }

        [Theory]
        [InlineData(null, "asset", "senderId", "receiverId")]
        [InlineData("transactionId", null, "senderId", "receiverId")]
        [InlineData("transactionId", "senderId", null, "receiverId")]
        [InlineData("transactionId", "senderId", "senderId", null)]
        public async Task TryToProcessTransferEvent_MissingDataInEvent_NotProcessed
                (string transactionId, string assetSymbol, string senderId, string receiverId)
        {
            _transfersRepoMock.Setup(x => x.AddAsync(It.IsAny<ITransfer>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var transfer = new Transfer
            {
                AssetSymbol = assetSymbol,
                TransactionId = transactionId,
                Amount = 100,
                SenderCustomerId = senderId,
                ReceiverCustomerId = receiverId
            };

            var operationsService = CreateSutInstance();

            await operationsService.ProcessTransferEventAsync(transfer);
            _transfersRepoMock.Verify(x => x.AddAsync(It.IsAny<ITransfer>()), Times.Never);
        }

        [Fact]
        public async Task TryToProcessBonusEvent_WithoutConditionId_SuccessfullyProcessedWithoutConditionName()
        {
            var campaignResponseModel = new CampaignDetailResponseModel { Name = "campaign" };
            _campaignClientMock.Setup(x => x.History.GetEarnRuleByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(campaignResponseModel);

            _bonusRepoMock.Setup(x => x.AddAsync(It.IsAny<BonusCashInDto>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var bonus = new BonusCashInDto
            {
                AssetSymbol = "asset",
                TransactionId = "transactionId",
                Amount = 100,
                CustomerId = "customerId",
                BonusType = "type",
                CampaignId = FakeCampaignId
            };

            var operationsService = CreateSutInstance();

            await operationsService.ProcessBonusReceivedEventAsync(bonus);
            _bonusRepoMock.Verify(x => x.AddAsync(It.Is<BonusCashInDto>(b => b.ConditionName == null)));
        }

        [Fact]
        public async Task TryToProcessBonusEvent_HasConditionId_SuccessfullyProcessedWithConditionName()
        {
            var campaignResponseModel = new CampaignDetailResponseModel
            {
                Name = "campaign",
                Conditions = new List<ConditionModel>
                {
                    new ConditionModel
                    {
                        Id = FakeConditionId,
                        TypeDisplayName = FakeConditionName
                    }
                }
            };
            _campaignClientMock.Setup(x => x.History.GetEarnRuleByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(campaignResponseModel);

            _bonusRepoMock.Setup(x => x.AddAsync(It.IsAny<BonusCashInDto>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var bonus = new BonusCashInDto
            {
                AssetSymbol = "asset",
                TransactionId = "transactionId",
                Amount = 100,
                CustomerId = "customerId",
                BonusType = "type",
                CampaignId = FakeCampaignId,
                ConditionId = FakeConditionId
            };

            var operationsService = CreateSutInstance();

            await operationsService.ProcessBonusReceivedEventAsync(bonus);
            _bonusRepoMock.Verify(x => x.AddAsync(It.Is<BonusCashInDto>(b => b.ConditionName == FakeConditionName)),
                Times.Once);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10_000_000_000)]
        public async Task TryToProcessBonusEvent_InvalidBalance_SuccessfullyProcessedWithWarning(long amount)
        {
            var campaignResponseModel = new CampaignDetailResponseModel { Name = "campaign" };
            _campaignClientMock.Setup(x => x.History.GetEarnRuleByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(campaignResponseModel);

            _bonusRepoMock.Setup(x => x.AddAsync(It.IsAny<BonusCashInDto>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var bonus = new BonusCashInDto
            {
                AssetSymbol = "asset",
                TransactionId = "transactionId",
                Amount = amount,
                CustomerId = "customerId",
                BonusType = "type",
                CampaignId = FakeCampaignId
            };

            var operationsService = CreateSutInstance();

            await operationsService.ProcessBonusReceivedEventAsync(bonus);
            _bonusRepoMock.Verify();
        }

        [Theory]
        [InlineData(null, "asset", "customerId", "type", FakeCampaignId)]
        [InlineData("transactionId", null, "customerId", "type", FakeCampaignId)]
        [InlineData("transactionId", "asset", null, "type", FakeCampaignId)]
        [InlineData("transactionId", "asset", "customerId", null, FakeCampaignId)]
        [InlineData("transactionId", "asset", "customerId", "type", null)]
        public async Task TryToProcessBonusEvent_MissingDataInEvent_NotProcessed
                (string transactionId, string assetSymbol, string customerId, string type, string campaignId)
        {
            var campaignResponseModel = new CampaignDetailResponseModel { Name = "campaign" };
            _campaignClientMock.Setup(x => x.History.GetEarnRuleByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(campaignResponseModel);

            _bonusRepoMock.Setup(x => x.AddAsync(It.IsAny<BonusCashInDto>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var bonus = new BonusCashInDto
            {
                AssetSymbol = assetSymbol,
                TransactionId = transactionId,
                Amount = 100,
                CustomerId = customerId,
                BonusType = type,
                CampaignId = campaignId
            };

            var operationsService = CreateSutInstance();

            await operationsService.ProcessBonusReceivedEventAsync(bonus);
            _bonusRepoMock.Verify(x => x.AddAsync(It.IsAny<BonusCashInDto>()), Times.Never);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10_000_000_000)]
        public async Task TryToProcessReferralStakeTokensReservedEventEvent_InvalidBalance_SuccessfullyProcessedWithWarning(long amount)
        {
            var campaignResponseModel = new CampaignDetailResponseModel { Name = "campaign" };
            _campaignClientMock.Setup(x => x.History.GetEarnRuleByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(campaignResponseModel);

            _referralStakesRepoMock.Setup(x => x.AddReferralStakeAsync(It.IsAny<ReferralStakeDto>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var bonus = new ReferralStakeDto
            {
                AssetSymbol = TokenSymbol,
                Amount = amount,
                CustomerId = "customerId",
                ReferralId = FakeReferralId,
                CampaignId = FakeCampaignId
            };

            var operationsService = CreateSutInstance();

            await operationsService.ProcessReferralStakeTokensReservationEventAsync(bonus);
            _referralStakesRepoMock.Verify();
        }

        [Theory]
        [InlineData(null, FakeSenderCustomerId, FakeCampaignId)]
        [InlineData(FakeReferralId, null, FakeCampaignId)]
        [InlineData(FakeReferralId, FakeSenderCustomerId, null)]
        public async Task TryToProcessReferralStakeTokensReservedEventEvent_MissingDataInEvent_NotProcessed
                (string referralId, string customerId, string campaignId)
        {
            var campaignResponseModel = new CampaignDetailResponseModel { Name = "campaign" };
            _campaignClientMock.Setup(x => x.History.GetEarnRuleByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(campaignResponseModel);

            _referralStakesRepoMock.Setup(x => x.AddReferralStakeAsync(It.IsAny<ReferralStakeDto>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var referralStakeDto = new ReferralStakeDto
            {
                AssetSymbol = TokenSymbol,
                Amount = 1,
                CustomerId = customerId,
                ReferralId = referralId,
                CampaignId = campaignId
            };

            var operationsService = CreateSutInstance();

            await operationsService.ProcessReferralStakeTokensReservationEventAsync(referralStakeDto);
            _referralStakesRepoMock.Verify(x => x.AddReferralStakeAsync(It.IsAny<ReferralStakeDto>()), Times.Never);
        }

        private OperationsService CreateSutInstance()
        {
            return new OperationsService(
                _transfersRepoMock.Object,
                _bonusRepoMock.Object,
                _campaignClientMock.Object,
                _pbfServiceClientMock.Object,
                _customerTierRepositoryMock.Object,
                new MemoryCache(new MemoryCacheOptions()),
                new TimeSpan(0, 0, 1),
                _partnersPaymentsRepoMock.Object,
                _referralStakesRepoMock.Object,
                _partnerManagementClientMock.Object,
                _linkedWalletTransfersRepositoryMock.Object,
                _feeCollectedOperationsRepo.Object,
                _linkWalletOperationsRepoMock.Object,
                _voucherPurchasePaymentsRepoMock.Object,
                _smartVoucherPaymentsRepoMock.Object,
                TokenSymbol,
                EmptyLogFactory.Instance);
        }
    }
}
