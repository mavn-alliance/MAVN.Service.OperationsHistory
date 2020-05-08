using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Cache;
using Lykke.Common.Log;
using MAVN.Service.Campaign.Client;
using MAVN.Service.Campaign.Client.Models.Enums;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.Domain.Services;
using MAVN.Service.PartnerManagement.Client;
using MAVN.Service.PrivateBlockchainFacade.Client;
using Microsoft.Extensions.Caching.Memory;

namespace MAVN.Service.OperationsHistory.DomainServices.Services
{
    public class OperationsService : IOperationsService
    {
        private readonly ITransfersRepository _transfersRepository;
        private readonly IBonusCashInsRepository _bonusCashInsRepository;
        private readonly ICampaignClient _campaignClient;
        private readonly IPrivateBlockchainFacadeClient _privateBlockchainFacadeClient;
        private readonly ICustomerTierRepository _customerTierRepository;
        private readonly OnDemandDataCache<string> _customerWalletsCache;
        private readonly TimeSpan _customerWalletsCacheExpirationPeriod;
        private readonly IPartnersPaymentsRepository _partnersPaymentsRepository;
        private readonly IPartnerManagementClient _partnerManagementClient;
        private readonly IReferralStakesRepository _referralStakesRepository;
        private readonly ILinkedWalletTransfersRepository _linkedWalletTransfersRepository;
        private readonly IFeeCollectedOperationsRepository _feeCollectedOperationsRepository;
        private readonly ILinkWalletOperationsRepository _linkWalletOperationsRepository;
        private readonly IVoucherPurchasePaymentsRepository _voucherPurchasePaymentsRepository;
        private readonly ISmartVoucherPaymentsRepository _smartVoucherPaymentsRepository;
        private readonly string _tokenSymbol;
        private readonly ILog _log;

        public OperationsService(ITransfersRepository transfersRepository,
            IBonusCashInsRepository bonusCashInsRepository,
            ICampaignClient campaignClient,
            IPrivateBlockchainFacadeClient privateBlockchainFacadeClient,
            ICustomerTierRepository customerTierRepository,
            IMemoryCache memoryCache,
            TimeSpan customerWalletsCacheExpirationPeriod,
            IPartnersPaymentsRepository partnersPaymentsRepository,
            IReferralStakesRepository referralStakesRepository,
            IPartnerManagementClient partnerManagementClient,
            ILinkedWalletTransfersRepository linkedWalletTransfersRepository,
            IFeeCollectedOperationsRepository feeCollectedOperationsRepository,
            ILinkWalletOperationsRepository linkWalletOperationsRepository,
            IVoucherPurchasePaymentsRepository voucherPurchasePaymentsRepository,
            ISmartVoucherPaymentsRepository smartVoucherPaymentsRepository,
            string tokenSymbol,
            ILogFactory logFactory)
        {
            _transfersRepository = transfersRepository;
            _bonusCashInsRepository = bonusCashInsRepository;
            _campaignClient = campaignClient;
            _privateBlockchainFacadeClient = privateBlockchainFacadeClient;
            _customerTierRepository = customerTierRepository;
            _customerWalletsCacheExpirationPeriod = customerWalletsCacheExpirationPeriod;
            _tokenSymbol = tokenSymbol;
            _partnersPaymentsRepository = partnersPaymentsRepository;
            _partnerManagementClient = partnerManagementClient;
            _referralStakesRepository = referralStakesRepository;
            _linkedWalletTransfersRepository = linkedWalletTransfersRepository;
            _feeCollectedOperationsRepository = feeCollectedOperationsRepository;
            _linkWalletOperationsRepository = linkWalletOperationsRepository;
            _voucherPurchasePaymentsRepository = voucherPurchasePaymentsRepository;
            _smartVoucherPaymentsRepository = smartVoucherPaymentsRepository;
            _customerWalletsCache = new OnDemandDataCache<string>(memoryCache);
            _log = logFactory.CreateLog(this);
        }

        public async Task ProcessTransferEventAsync(ITransfer transfer)
        {
            var isValid = ValidateTransfer(transfer);

            if (!isValid)
                return;

            // fetching additional data
            var getSenderWalletAddressTask = GetCachedCustomerWalletAddressAsync(transfer.SenderCustomerId);
            var getRecipientWalletAddressTask = GetCachedCustomerWalletAddressAsync(transfer.ReceiverCustomerId);
            await Task.WhenAll(getSenderWalletAddressTask, getRecipientWalletAddressTask);

            // extending transfer event with additional data
            transfer.SenderWalletAddress = getSenderWalletAddressTask.Result;
            transfer.ReceiverWalletAddress = getRecipientWalletAddressTask.Result;

            await _transfersRepository.AddAsync(transfer);
        }

        public async Task ProcessBonusReceivedEventAsync(BonusCashInDto bonusCashIn)
        {
            var isValid = ValidateBonus(bonusCashIn);

            if (!isValid)
                return;

            var campaign = await _campaignClient.History.GetEarnRuleByIdAsync(Guid.Parse(bonusCashIn.CampaignId));

            if (campaign.ErrorCode == CampaignServiceErrorCodes.EntityNotFound)
            {
                _log.Warning("Campaign not found for bonus cash in event", context: bonusCashIn);
                bonusCashIn.CampaignName = "N/A";
            }
            else
            {
                bonusCashIn.CampaignName = campaign.Name;

                if (!string.IsNullOrEmpty(bonusCashIn.ConditionId))
                {
                    var condition = campaign.Conditions.FirstOrDefault(c => c.Id == bonusCashIn.ConditionId);

                    if (condition == null)
                    {
                        _log.Warning(message: "Campaign does not contain the condition",
                            context: new { bonusCashIn.CampaignId, bonusCashIn.ConditionId });
                    }
                    bonusCashIn.ConditionName = condition?.TypeDisplayName;
                }
            }

            await _bonusCashInsRepository.AddAsync(bonusCashIn);
        }

        public async Task ProcessRefundPartnersPaymentEventAsync(PartnerPaymentDto partnerPayment)
        {
            var isValid = ValidatePartnerPayment(partnerPayment);

            if (!isValid)
                return;

            await AddAdditionalDataToPartnerPayment(partnerPayment);

            await _partnersPaymentsRepository.AddPartnerPaymentRefundAsync(partnerPayment);
        }

        public async Task ProcessTransferToLinkedEvent(LinkedWalletTransferDto transfer)
        {
            var isValid = ValidateTransferToLinkedWallet(transfer);

            if (!isValid)
                return;

            AddAdditionalDataToPublicWalletTransfer(transfer, LinkedWalletTransferDirection.Outgoing);

            await _linkedWalletTransfersRepository.AddAsync(transfer);
        }

        public async Task ProcessTransferFromLinkedToInternalEvent(LinkedWalletTransferDto transfer)
        {
            var isValid = ValidateTransferFromLinkedToInternalWallet(transfer);

            if (!isValid)
                return;

            AddAdditionalDataToPublicWalletTransfer(transfer, LinkedWalletTransferDirection.Incoming);

            await _linkedWalletTransfersRepository.AddAsync(transfer);
        }

        public async Task ProcessPartnersPaymentTokensReservedEventAsync(PartnerPaymentDto partnerPayment)
        {
            var isValid = ValidatePartnerPayment(partnerPayment);

            if (!isValid)
                return;

            await AddAdditionalDataToPartnerPayment(partnerPayment);

            await _partnersPaymentsRepository.AddPartnerPaymentAsync(partnerPayment);
        }

        public Task ProcessCustomerTierChangedEventAsync(CustomerTierModel customerTier)
        {
            return _customerTierRepository.InsertAsync(customerTier);
        }

        public async Task ProcessReferralStakeTokensReservationEventAsync(ReferralStakeDto referralStake)
        {
            var isValid = ValidateReferralStake(referralStake);

            if (!isValid)
                return;

            await AddAdditionalDataToReferralStake(referralStake);

            await _referralStakesRepository.AddReferralStakeAsync(referralStake);
        }

        public async Task ProcessReferralStakeTokensReleaseEventAsync(ReferralStakeDto referralStake)
        {
            var isValid = ValidateReferralStake(referralStake);

            if (!isValid)
                return;

            await AddAdditionalDataToReferralStake(referralStake);

            await _referralStakesRepository.AddReferralStakeReleasedAsync(referralStake);
        }

        public async Task ProcessFeeCollectedAsync(FeeCollectedOperationDto feeCollectedOperation)
        {
            var isValid = ValidateFeeCollectedOperation(feeCollectedOperation);

            if (!isValid)
                return;

            feeCollectedOperation.AssetSymbol = _tokenSymbol;
            feeCollectedOperation.Timestamp = DateTime.UtcNow;

            await _feeCollectedOperationsRepository.AddAsync(feeCollectedOperation);
        }

        public async Task ProcessWalletLinkingStateChangeCompletedEventAsync(LinkWalletOperationDto linkWalletOperation)
        {
            var isValid = ValidateLinkWalletOperation(linkWalletOperation);

            if (!isValid)
                return;

            linkWalletOperation.Direction = string.IsNullOrEmpty(linkWalletOperation.PublicAddress)
                ? LinkWalletDirection.Unlink
                : LinkWalletDirection.Link;

            linkWalletOperation.AssetSymbol = _tokenSymbol;

            await _linkWalletOperationsRepository.AddAsync(linkWalletOperation);
        }

        public Task ProcessVoucherTokensUsedEventAsync(VoucherPurchasePaymentDto voucherPurchasePaymentOperation)
        {
            voucherPurchasePaymentOperation.AssetSymbol = _tokenSymbol;

            return _voucherPurchasePaymentsRepository.InsertAsync(voucherPurchasePaymentOperation);
        }

        public async Task ProcessSmartVoucherSoldEventAsync(SmartVoucherPaymentDto smartVoucherPayment)
        {
            var isValid = ValidateSmartVoucherPaymentOperation(smartVoucherPayment);

            if (!isValid)
                return;

            await _smartVoucherPaymentsRepository.AddAsync(smartVoucherPayment);
        }

        private async Task AddAdditionalDataToReferralStake(ReferralStakeDto referralStake)
        {
            referralStake.AssetSymbol = _tokenSymbol;

            var campaign = await _campaignClient.History.GetEarnRuleByIdAsync(Guid.Parse(referralStake.CampaignId));

            if (campaign.ErrorCode == CampaignServiceErrorCodes.EntityNotFound)
            {
                _log.Warning("Campaign not found for referral stake in event", context: referralStake);
                referralStake.CampaignName = "N/A";
            }
            else
            {
                referralStake.CampaignName = campaign.Name;
            }
        }

        private async Task AddAdditionalDataToPartnerPayment(PartnerPaymentDto partnerPaymentDto)
        {
            partnerPaymentDto.AssetSymbol = _tokenSymbol;

            var partner = await _partnerManagementClient.Partners.GetByIdAsync(Guid.Parse(partnerPaymentDto.PartnerId));

            if (partner == null)
            {
                _log.Warning("Partner not found for Partner Payment", context: partnerPaymentDto);
                partnerPaymentDto.PartnerName = "N/A";
            }
            else
            {
                partnerPaymentDto.PartnerName = partner.Name;
            }
        }

        private void AddAdditionalDataToPublicWalletTransfer(LinkedWalletTransferDto transfer, LinkedWalletTransferDirection direction)
        {
            transfer.AssetSymbol = _tokenSymbol;
            transfer.Direction = direction;
            transfer.Timestamp = DateTime.UtcNow;
        }

        #region Validation
        private bool ValidateTransfer(ITransfer transfer)
        {
            var isValid = true;

            if (transfer.Amount <= 0 || transfer.Amount > 1_000_000_000)
            {
                _log.Warning("Processed transfer event with invalid amount", context: transfer);
            }

            if (string.IsNullOrEmpty(transfer.TransactionId))
            {
                isValid = false;
                _log.Warning("Transfer event without transaction id", context: transfer);
            }

            if (string.IsNullOrEmpty(transfer.AssetSymbol))
            {
                isValid = false;
                _log.Warning("Transfer event without asset symbol", context: transfer);
            }

            if (string.IsNullOrEmpty(transfer.SenderCustomerId))
            {
                isValid = false;
                _log.Warning("Transfer event without sender id", context: transfer);
            }

            if (string.IsNullOrEmpty(transfer.ReceiverCustomerId))
            {
                isValid = false;
                _log.Warning("Transfer event without receiver id", context: transfer);
            }

            return isValid;
        }

        private bool ValidateBonus(BonusCashInDto bonus)
        {
            var isValid = true;

            if (bonus.Amount <= 0 || bonus.Amount > 1_000_000_000)
            {
                _log.Warning("Processed bonus event with invalid balance", context: bonus);
            }

            if (string.IsNullOrEmpty(bonus.TransactionId))
            {
                isValid = false;
                _log.Warning("Bonus event without transaction id", context: bonus);
            }

            if (string.IsNullOrEmpty(bonus.AssetSymbol))
            {
                isValid = false;
                _log.Warning("Bonus event without asset symbol", context: bonus);
            }

            if (string.IsNullOrEmpty(bonus.CustomerId))
            {
                isValid = false;
                _log.Warning("Bonus event without customer id", context: bonus);
            }

            if (string.IsNullOrEmpty(bonus.BonusType))
            {
                isValid = false;
                _log.Warning("Bonus event without bonus type", context: bonus);
            }

            if (string.IsNullOrEmpty(bonus.CampaignId))
            {
                isValid = false;
                _log.Warning("Bonus event without campaign id", context: bonus);
            }

            return isValid;
        }

        private bool ValidatePartnerPayment(PartnerPaymentDto partnerPayment)
        {

            var isValid = true;
            if (partnerPayment.Amount <= 0)
            {
                _log.Warning("Processed partner payment with invalid amount", context: partnerPayment);
            }

            if (string.IsNullOrEmpty(partnerPayment.PaymentRequestId))
            {
                isValid = false;
                _log.Warning("Partner payment without id", context: partnerPayment);
            }

            if (string.IsNullOrEmpty(partnerPayment.CustomerId))
            {
                isValid = false;
                _log.Warning("Partner payment without customer id", context: partnerPayment);
            }

            if (string.IsNullOrEmpty(partnerPayment.PartnerId))
            {
                isValid = false;
                _log.Warning("Partner payment event invoice id type", context: partnerPayment);
            }

            return isValid;
        }

        private bool ValidateReferralStake(ReferralStakeDto referralStake)
        {
            var isValid = true;
            if (referralStake.Amount <= 0)
            {
                _log.Warning("Processed referral stake with invalid amount", context: referralStake);
            }

            if (string.IsNullOrEmpty(referralStake.ReferralId))
            {
                isValid = false;
                _log.Warning("Referral stake without id", context: referralStake);
            }

            if (string.IsNullOrEmpty(referralStake.CustomerId))
            {
                isValid = false;
                _log.Warning("Referral stake without customer id", context: referralStake);
            }

            if (string.IsNullOrEmpty(referralStake.CampaignId))
            {
                isValid = false;
                _log.Warning("Referral stake without campaign id", context: referralStake);
            }

            return isValid;
        }

        private bool ValidateTransferToLinkedWallet(LinkedWalletTransferDto transfer)
        {
            bool isValid = true;
            if (transfer.Amount <= 0)
            {
                isValid = false;
                _log.Warning("Processed transfer to linked wallet with invalid amount", context: transfer);
            }

            if (string.IsNullOrEmpty(transfer.CustomerId))
            {
                isValid = false;
                _log.Warning("Transfer to linked wallet without customer id", context: transfer);
            }

            if (string.IsNullOrEmpty(transfer.PrivateAddress))
            {
                isValid = false;
                _log.Warning("Transfer to linked wallet without private address", context: transfer);
            }

            if (string.IsNullOrEmpty(transfer.PublicAddress))
            {
                isValid = false;
                _log.Warning("Transfer to linked wallet without public address", context: transfer);
            }

            if (string.IsNullOrEmpty(transfer.OperationId))
            {
                isValid = false;
                _log.Warning("Transfer to linked to internal without operationId", context: transfer);
            }

            return isValid;
        }

        private bool ValidateTransferFromLinkedToInternalWallet(LinkedWalletTransferDto transfer)
        {
            bool isValid = true;
            if (transfer.Amount <= 0)
            {
                isValid = false;
                _log.Warning("Processed transfer from linked to internal with invalid amount", context: transfer);
            }

            if (string.IsNullOrEmpty(transfer.CustomerId))
            {
                isValid = false;
                _log.Warning("Transfer from linked to internal without customer id", context: transfer);
            }

            if (string.IsNullOrEmpty(transfer.PrivateAddress))
            {
                isValid = false;
                _log.Warning("Transfer from linked to internal without private address", context: transfer);
            }

            if (string.IsNullOrEmpty(transfer.PublicAddress))
            {
                isValid = false;
                _log.Warning("Transfer from linked to internal without public address", context: transfer);
            }

            if (string.IsNullOrEmpty(transfer.OperationId))
            {
                isValid = false;
                _log.Warning("Transfer from linked to internal without operationId", context: transfer);
            }

            return isValid;
        }

        private bool ValidateFeeCollectedOperation(FeeCollectedOperationDto feeCollectedOperation)
        {
            bool isValid = true;

            if (feeCollectedOperation.Fee < 0)
            {
                isValid = false;
                _log.Warning("Fee Collected operation with negative fee amount", context: feeCollectedOperation);
            }

            if (string.IsNullOrEmpty(feeCollectedOperation.CustomerId))
            {
                isValid = false;
                _log.Warning("Fee Collected operation without customer id", context: feeCollectedOperation);
            }

            if (string.IsNullOrEmpty(feeCollectedOperation.OperationId))
            {
                isValid = false;
                _log.Warning("Fee Collected operation without operationId", context: feeCollectedOperation);
            }

            return isValid;
        }

        private bool ValidateLinkWalletOperation(LinkWalletOperationDto linkWalletOperation)
        {
            bool isValid = true;

            if (linkWalletOperation.Fee < 0)
            {
                isValid = false;
                _log.Warning("Link wallet operation with negative fee amount", context: linkWalletOperation);
            }

            if (string.IsNullOrEmpty(linkWalletOperation.CustomerId))
            {
                isValid = false;
                _log.Warning("Link wallet operation without customer id", context: linkWalletOperation);
            }

            if (string.IsNullOrEmpty(linkWalletOperation.PrivateAddress))
            {
                isValid = false;
                _log.Warning("Link wallet operation without private address", context: linkWalletOperation);
            }

            if (string.IsNullOrEmpty(linkWalletOperation.OperationId))
            {
                isValid = false;
                _log.Warning("Link wallet operation without operationId", context: linkWalletOperation);
            }

            return isValid;
        }

        private bool ValidateSmartVoucherPaymentOperation(SmartVoucherPaymentDto smartVoucherPayment)
        {
            bool isValid = true;

            if (smartVoucherPayment.Amount < 0)
            {
                isValid = false;
                _log.Warning("Smart voucher payment with negative fee amount", context: smartVoucherPayment);
            }

            if (smartVoucherPayment.CustomerId == Guid.Empty)
            {
                isValid = false;
                _log.Warning("Smart voucher payment without customer id", context: smartVoucherPayment);
            }

            if (smartVoucherPayment.CampaignId == Guid.Empty)
            {
                isValid = false;
                _log.Warning("Smart voucher payment without campaign id", context: smartVoucherPayment);
            }

            if (string.IsNullOrEmpty(smartVoucherPayment.ShortCode))
            {
                isValid = false;
                _log.Warning("Smart voucher payment without short code", context: smartVoucherPayment);
            }

            if (string.IsNullOrEmpty(smartVoucherPayment.PaymentRequestId))
            {
                isValid = false;
                _log.Warning("Smart voucher payment without payment request id", context: smartVoucherPayment);
            }

            if (smartVoucherPayment.PartnerId == Guid.Empty)
            {
                isValid = false;
                _log.Warning("Smart voucher payment without partner id", context: smartVoucherPayment);
            }

            if (string.IsNullOrEmpty(smartVoucherPayment.AssetSymbol))
            {
                isValid = false;
                _log.Warning("Smart voucher payment without asset symbol", context: smartVoucherPayment);
            }

            return isValid;
        }

        #endregion

        private async Task<string> GetCustomerWalletAddressAsync(string customerId)
        {
            var response = await _privateBlockchainFacadeClient.CustomersApi.GetWalletAddress(Guid.Parse(customerId));
            return response.WalletAddress;
        }

        private Task<string> GetCachedCustomerWalletAddressAsync(string customerId)
        {
            return _customerWalletsCache.GetOrAddAsync(
                customerId,
                GetCustomerWalletAddressAsync,
                _customerWalletsCacheExpirationPeriod);
        }
    }
}
