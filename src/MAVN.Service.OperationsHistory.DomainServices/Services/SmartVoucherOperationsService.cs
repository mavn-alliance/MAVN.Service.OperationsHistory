using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.Domain.Services;
using MAVN.Service.PartnerManagement.Client;
using MAVN.Service.SmartVouchers.Client;

namespace MAVN.Service.OperationsHistory.DomainServices.Services
{
    public class SmartVoucherOperationsService : ISmartVoucherOperationsService
    {
        private readonly ISmartVoucherRepository _smartVoucherRepository;
        private readonly ISmartVouchersClient _smartVouchersClient;
        private readonly IPartnerManagementClient _partnerManagementClient;
        private readonly ILog _log;

        public SmartVoucherOperationsService(
            ISmartVoucherRepository smartVoucherRepository,
            ISmartVouchersClient smartVouchersClient,
            IPartnerManagementClient partnerManagementClient,
            ILogFactory logFactory)
        {
            _smartVoucherRepository = smartVoucherRepository;
            _smartVouchersClient = smartVouchersClient;
            _partnerManagementClient = partnerManagementClient;
            _log = logFactory.CreateLog(this);
        }

        public async Task ProcessSmartVoucherSoldEventAsync(SmartVoucherPaymentDto smartVoucherPayment)
        {
            var isValid = ValidateSmartVoucherPaymentOperation(smartVoucherPayment);

            if (!isValid)
                return;

            await _smartVoucherRepository.AddPaymentAsync(smartVoucherPayment);
        }

        public async Task ProcessSmartVoucherUsedEventAsync(SmartVoucherUseDto smartVoucherUse)
        {
            smartVoucherUse.Id = Guid.NewGuid().ToString();

            var isValid = ValidateSmartVoucherUseOperation(smartVoucherUse);

            if (!isValid)
                return;

            await _smartVoucherRepository.AddUseAsync(smartVoucherUse);
        }

        public async Task ProcessSmartVoucherTransferredEventAsync(SmartVoucherTransferDto smartVoucherTransfer)
        {
            smartVoucherTransfer.Id = Guid.NewGuid().ToString();

            var isValid = ValidateSmartVoucherTransferOperation(smartVoucherTransfer);

            if (!isValid)
                return;

            var (partnerName, vertical, campaignName) = await GetAdditionalDataForSmartVoucher(
                smartVoucherTransfer.CampaignId,
                smartVoucherTransfer.PartnerId);

            smartVoucherTransfer.PartnerName = partnerName;
            smartVoucherTransfer.Vertical = vertical;
            smartVoucherTransfer.CampaignName = campaignName;

            await _smartVoucherRepository.AddTransferAsync(smartVoucherTransfer);
        }

        private async Task<(string PartnerName, string Vertical, string CampaignName)> GetAdditionalDataForSmartVoucher(string campaignId, Guid partnerId)
        {
            var partner = await _partnerManagementClient.Partners.GetByIdAsync(partnerId);
            var campaign = await _smartVouchersClient.CampaignsApi.GetByIdAsync(Guid.Parse(campaignId));

            return (partner?.Name, partner?.BusinessVertical?.ToString(), campaign?.Name);
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

            if (string.IsNullOrEmpty(smartVoucherPayment.CampaignId))
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

        private bool ValidateSmartVoucherUseOperation(SmartVoucherUseDto smartVoucherUse)
        {
            bool isValid = true;

            if (smartVoucherUse.Amount < 0)
            {
                isValid = false;
                _log.Warning("Smart voucher use with negative fee amount", context: smartVoucherUse);
            }

            if (smartVoucherUse.CustomerId == Guid.Empty)
            {
                isValid = false;
                _log.Warning("Smart voucher use without customer id", context: smartVoucherUse);
            }

            if (string.IsNullOrEmpty(smartVoucherUse.CampaignId))
            {
                isValid = false;
                _log.Warning("Smart voucher use without campaign id", context: smartVoucherUse);
            }

            if (smartVoucherUse.PartnerId == Guid.Empty)
            {
                isValid = false;
                _log.Warning("Smart voucher use without partner id", context: smartVoucherUse);
            }

            if (string.IsNullOrEmpty(smartVoucherUse.AssetSymbol))
            {
                isValid = false;
                _log.Warning("Smart voucher use without asset symbol", context: smartVoucherUse);
            }

            return isValid;
        }

        private bool ValidateSmartVoucherTransferOperation(SmartVoucherTransferDto smartVoucherTransfer)
        {
            bool isValid = true;

            if (smartVoucherTransfer.Amount < 0)
            {
                isValid = false;
                _log.Warning("Smart voucher transfer with negative fee amount", context: smartVoucherTransfer);
            }

            if (smartVoucherTransfer.OldCustomerId == Guid.Empty)
            {
                isValid = false;
                _log.Warning("Smart voucher transfer without customer id", context: smartVoucherTransfer);
            }

            if (smartVoucherTransfer.NewCustomerId == Guid.Empty)
            {
                isValid = false;
                _log.Warning("Smart voucher transfer without customer id", context: smartVoucherTransfer);
            }

            if (string.IsNullOrEmpty(smartVoucherTransfer.CampaignId))
            {
                isValid = false;
                _log.Warning("Smart voucher transfer without campaign id", context: smartVoucherTransfer);
            }

            if (smartVoucherTransfer.PartnerId == Guid.Empty)
            {
                isValid = false;
                _log.Warning("Smart voucher transfer without partner id", context: smartVoucherTransfer);
            }

            if (string.IsNullOrEmpty(smartVoucherTransfer.AssetSymbol))
            {
                isValid = false;
                _log.Warning("Smart voucher transfer without asset symbol", context: smartVoucherTransfer);
            }

            return isValid;
        }
    }
}
