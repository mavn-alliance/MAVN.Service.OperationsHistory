using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.Domain.Services;

namespace MAVN.Service.OperationsHistory.DomainServices.Services
{
    public class OperationsQueryService : IOperationsQueryService
    {
        private readonly ITransactionHistoryRepository _transactionHistoryRepository;
        private readonly IBonusCashInsRepository _bonusCashInsRepository;
        private readonly IPartnersPaymentsRepository _partnersPaymentsRepository;
        private readonly IVoucherPurchasePaymentsRepository _voucherPurchasePaymentsRepository;
        private readonly ICustomerProfileClient _customerProfileClient;
        private readonly ISmartVoucherRepository _smartVoucherRepository;
        private readonly ILog _log;

        public OperationsQueryService(
            ITransactionHistoryRepository transactionHistoryRepository,
            IBonusCashInsRepository bonusCashInsRepository,
            IPartnersPaymentsRepository partnersPaymentsRepository,
            IVoucherPurchasePaymentsRepository voucherPurchasePaymentsRepository,
            ICustomerProfileClient customerProfileClient,
            ISmartVoucherRepository smartVoucherRepository,
            ILogFactory logFactory)
        {
            _transactionHistoryRepository = transactionHistoryRepository;
            _bonusCashInsRepository = bonusCashInsRepository;
            _partnersPaymentsRepository = partnersPaymentsRepository;
            _voucherPurchasePaymentsRepository = voucherPurchasePaymentsRepository;
            _customerProfileClient = customerProfileClient;
            _smartVoucherRepository = smartVoucherRepository;
            _log = logFactory.CreateLog(this);
        }

        public async Task<PaginatedCustomerOperationsModel> GetByCustomerIdPaginatedAsync(string customerId, int currentPage, int pageSize)
        {
            var (skip, take) = ValidateAndCalculateSkipAndTake(currentPage, pageSize);

            var result = await _transactionHistoryRepository.GetByCustomerIdPaginatedAsync(customerId, skip, take);

            await SetCustomersEmails(result.Transfers);
            await SetCustomersEmails(result.SmartVoucherTransfers);

            return result;
        }

        public Task<PaginatedTransactionHistory> GetByDatePaginatedAsync(DateTime fromDate, DateTime toDate, int currentPage, int pageSize)
        {
            if (fromDate >= toDate)
                throw new InvalidOperationException($"{nameof(fromDate)} must be earlier than {nameof(toDate)}");

            var (skip, take) = ValidateAndCalculateSkipAndTake(currentPage, pageSize);

            return _transactionHistoryRepository.GetByDatePaginatedAsync(fromDate, toDate, skip, take);
        }

        public Task<PaginatedBonusesHistory> GetBonusesByDatesPaginatedAsync(
            DateTime fromDate,
            DateTime toDate,
            int currentPage,
            int pageSize)
        {
            if (fromDate >= toDate)
                throw new InvalidOperationException($"{nameof(fromDate)} must be earlier than {nameof(toDate)}");

            var (skip, take) = ValidateAndCalculateSkipAndTake(currentPage, pageSize);

            return _bonusCashInsRepository.GetByDatesPaginatedAsync(fromDate, toDate, skip, take);
        }

        public Task<PaginatedPartnersPaymentsHistory> GetPartnersPaymentsByDatesPaginatedAsync(
            DateTime fromDate,
            DateTime toDate,
            int currentPage,
            int pageSize)
        {
            if (fromDate >= toDate)
                throw new InvalidOperationException($"{nameof(fromDate)} must be earlier than {nameof(toDate)}");

            var (skip, take) = ValidateAndCalculateSkipAndTake(currentPage, pageSize);

            return _partnersPaymentsRepository.GetByDatesPaginatedAsync(fromDate, toDate, skip, take);
        }

        public Task<PaginatedVoucherPurchasePaymentsHistory> GetVoucherPurchasePaymentsByDatesPaginatedAsync(
            DateTime fromDate,
            DateTime toDate,
            int currentPage,
            int pageSize)
        {
            if (fromDate >= toDate)
                throw new InvalidOperationException($"{nameof(fromDate)} must be earlier than {nameof(toDate)}");

            var (skip, take) = ValidateAndCalculateSkipAndTake(currentPage, pageSize);

            return _voucherPurchasePaymentsRepository.GetByDatesPaginatedAsync(fromDate, toDate, skip, take);
        }

        public Task<PaginatedSmartVoucherPaymentsHistory> GetSmartVoucherPaymentsByDatesPaginatedAsync(
            DateTime fromDate,
            DateTime toDate,
            int currentPage,
            int pageSize)
        {
            if (fromDate >= toDate)
                throw new InvalidOperationException($"{nameof(fromDate)} must be earlier than {nameof(toDate)}");

            var (skip, take) = ValidateAndCalculateSkipAndTake(currentPage, pageSize);

            return _smartVoucherRepository.GetByDatesPaginatedAsync(fromDate, toDate, skip, take);
        }

        public async Task<IEnumerable<IBonusCashIn>> GetBonusCashInsAsync(string customerId, string campaignId)
        {
            return await _transactionHistoryRepository.GetBonusCashInsAsync(customerId, campaignId);
        }

        public async Task<IEnumerable<IBonusCashIn>> GetBonusCashInsByReferralAsync(string customerId, string referralId)
        {
            return await _transactionHistoryRepository.GetBonusCashInsByReferralAsync(customerId, referralId);
        }

        private async Task SetCustomersEmails(IEnumerable<Transfer> transfers)
        {
            var customerIds = new HashSet<string>();

            foreach (var transfer in transfers)
            {
                customerIds.Add(transfer.SenderCustomerId);
                customerIds.Add(transfer.ReceiverCustomerId);
            }

            var customerProfiles = await _customerProfileClient.CustomerProfiles.GetByIdsAsync(
                customerIds.ToArray(),
                includeNotVerified: true,
                includeNotActive: true);

            var customerEmails = customerProfiles.ToDictionary(x => x.CustomerId, x => x.Email);

            foreach (var transfer in transfers)
            {
                var senderCustomerExists = customerEmails.TryGetValue(transfer.SenderCustomerId, out var senderEmail);
                var receiverCustomerExists = customerEmails.TryGetValue(transfer.ReceiverCustomerId, out var receiverEmail);

                if (!senderCustomerExists)
                    _log.Error(message: "Sender customer does not exist for already processed Transfer", context: new { transfer.SenderCustomerId, transfer.TransactionId });

                if (!receiverCustomerExists)
                    _log.Error(message: "Receiver customer does not exist for already processed Transfer", context: new { transfer.ReceiverCustomerId, transfer.TransactionId });

                transfer.SenderCustomerEmail = senderEmail;
                transfer.ReceiverCustomerEmail = receiverEmail;
            }
        }

        private async Task SetCustomersEmails(IEnumerable<SmartVoucherTransferDto> transfers)
        {
            var customerIds = new HashSet<string>();

            foreach (var transfer in transfers)
            {
                customerIds.Add(transfer.OldCustomerId.ToString());
                customerIds.Add(transfer.NewCustomerId.ToString());
            }

            var customerProfiles = await _customerProfileClient.CustomerProfiles.GetByIdsAsync(
                customerIds.ToArray(),
                includeNotVerified: true,
                includeNotActive: true);

            var customerEmails = customerProfiles.ToDictionary(x => x.CustomerId, x => x.Email);

            foreach (var transfer in transfers)
            {
                var senderCustomerExists = customerEmails.TryGetValue(transfer.OldCustomerId.ToString(), out var senderEmail);
                var receiverCustomerExists = customerEmails.TryGetValue(transfer.NewCustomerId.ToString(), out var receiverEmail);

                if (!senderCustomerExists)
                    _log.Error(message: "Sender customer does not exist for already processed Smart Voucher Transfer", context: transfer.OldCustomerId);

                if (!receiverCustomerExists)
                    _log.Error(message: "Receiver customer does not exist for already processed Smart VoucherTransfer", context: transfer.NewCustomerId);

                transfer.OldCustomerEmail = senderEmail;
                transfer.NewCustomerEmail = receiverEmail;
            }
        }

        private (int skip, int take) ValidateAndCalculateSkipAndTake(int currentPage, int pageSize)
        {
            if (currentPage < 1)
                throw new ArgumentException("Current must be positive", nameof(currentPage));

            if (pageSize == 0)
                throw new ArgumentException("Page size can't be 0", nameof(pageSize));

            var skip = (currentPage - 1) * pageSize;
            var take = pageSize;

            return (skip, take);
        }
    }
}
