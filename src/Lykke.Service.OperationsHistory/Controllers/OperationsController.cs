using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Service.OperationsHistory.Client;
using Lykke.Service.OperationsHistory.Client.Models.Requests;
using Lykke.Service.OperationsHistory.Client.Models.Responses;
using Lykke.Service.OperationsHistory.Domain.Exceptions;
using Lykke.Service.OperationsHistory.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.OperationsHistory.Controllers
{
    [Route("api")]
    public class OperationsController : Controller, IOperationsHistoryApi
    {
        private readonly IOperationsQueryService _operationsQueryService;
        private readonly IMapper _mapper;

        public OperationsController(IOperationsQueryService operationsQueryService, IMapper mapper)
        {
            _operationsQueryService = operationsQueryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets paged transactions history by customer id
        /// </summary>
        /// <returns><see cref="PaginatedCustomerOperationsResponse"/></returns>
        [HttpGet("transactions/{customerId}")]
        [ProducesResponseType(typeof(PaginatedCustomerOperationsResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<PaginatedCustomerOperationsResponse> GetByCustomerIdAsync([Required][FromRoute] string customerId, PaginationModel paginationModel)
        {
            var result = await _operationsQueryService
                .GetByCustomerIdPaginatedAsync(customerId, paginationModel.CurrentPage, paginationModel.PageSize);

            return _mapper.Map<PaginatedCustomerOperationsResponse>(result);
        }

        /// <summary>
        /// Gets paged transactions history between two dates
        /// </summary>
        /// <returns><see cref="PaginatedTransactionHistoryResponse"/></returns>
        [HttpGet("transactions")]
        [ProducesResponseType(typeof(PaginatedTransactionHistoryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<PaginatedTransactionHistoryResponse> GetByDateAsync(PaginationModelWithDatesRange paginationModel)
        {
            if (paginationModel.FromDate >= paginationModel.ToDate)
                throw new BadRequestException($"{nameof(paginationModel.FromDate)} must be earlier than {nameof(paginationModel.ToDate)}");

            var result = await _operationsQueryService
                .GetByDatePaginatedAsync(paginationModel.FromDate, paginationModel.ToDate, paginationModel.CurrentPage, paginationModel.PageSize);

            return _mapper.Map<PaginatedTransactionHistoryResponse>(result);
        }

        /// <summary>
        /// Gets paged bonuses history between two dates
        /// </summary>
        /// <returns><see cref="PaginatedBonusesHistoryResponse"/></returns>
        [HttpGet("bonuses")]
        [ProducesResponseType(typeof(PaginatedBonusesHistoryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<PaginatedBonusesHistoryResponse> GetBonusesByDatesAsync(
            PaginationModelWithDatesRange paginationModel)
        {
            if (paginationModel.FromDate >= paginationModel.ToDate)
                throw new BadRequestException($"{nameof(paginationModel.FromDate)} must be earlier than {nameof(paginationModel.ToDate)}");

            var result = await _operationsQueryService.GetBonusesByDatesPaginatedAsync(
                paginationModel.FromDate,
                paginationModel.ToDate,
                paginationModel.CurrentPage,
                paginationModel.PageSize);

            return _mapper.Map<PaginatedBonusesHistoryResponse>(result);
        }

        /// <summary>
        /// Gets paged payment tarnsfers history between two dates
        /// </summary>
        /// <returns><see cref="PaginatedPaymentTransfersHistoryResponse"/></returns>
        [HttpGet("payment-transfers")]
        [ProducesResponseType(typeof(PaginatedPaymentTransfersHistoryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<PaginatedPaymentTransfersHistoryResponse> GetPaymentTransfersByDatesAsync(
            PaginationModelWithDatesRange paginationModel)
        {
            if (paginationModel.FromDate >= paginationModel.ToDate)
                throw new BadRequestException($"{nameof(paginationModel.FromDate)} must be earlier than {nameof(paginationModel.ToDate)}");

            var result = await _operationsQueryService.GetPaymentTransfersByDatesPaginatedAsync(
                paginationModel.FromDate,
                paginationModel.ToDate,
                paginationModel.CurrentPage,
                paginationModel.PageSize);

            return _mapper.Map<PaginatedPaymentTransfersHistoryResponse>(result);
        }

        /// <summary>
        /// Gets paged partners payments history between two dates
        /// </summary>
        /// <returns><see cref="PaginatedPartnersPaymentsHistoryResponse"/></returns>
        [HttpGet("partners-payments")]
        [ProducesResponseType(typeof(PaginatedPartnersPaymentsHistoryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<PaginatedPartnersPaymentsHistoryResponse> GetPartnersPaymentsByDatesAsync(
            PaginationModelWithDatesRange paginationModel)
        {
            if (paginationModel.FromDate >= paginationModel.ToDate)
                throw new BadRequestException($"{nameof(paginationModel.FromDate)} must be earlier than {nameof(paginationModel.ToDate)}");

            var result = await _operationsQueryService.GetPartnersPaymentsByDatesPaginatedAsync(
                paginationModel.FromDate,
                paginationModel.ToDate,
                paginationModel.CurrentPage,
                paginationModel.PageSize);

            return _mapper.Map<PaginatedPartnersPaymentsHistoryResponse>(result);
        }

        /// <summary>
        /// Gets paged voucher purchase payments history between two dates
        /// </summary>
        /// <returns><see cref="PaginatedPartnersPaymentsHistoryResponse"/></returns>
        [HttpGet("voucher-purchases")]
        [ProducesResponseType(typeof(PaginatedVoucherPurchasePaymentsHistoryResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<PaginatedVoucherPurchasePaymentsHistoryResponse> GetVoucherPurchasePaymentsByDatesAsync(
            PaginationModelWithDatesRange paginationModel)
        {
            if (paginationModel.FromDate >= paginationModel.ToDate)
                throw new BadRequestException($"{nameof(paginationModel.FromDate)} must be earlier than {nameof(paginationModel.ToDate)}");

            var result = await _operationsQueryService.GetVoucherPurchasePaymentsByDatesPaginatedAsync(
                paginationModel.FromDate,
                paginationModel.ToDate,
                paginationModel.CurrentPage,
                paginationModel.PageSize);

            return _mapper.Map<PaginatedVoucherPurchasePaymentsHistoryResponse>(result);
        }

        /// <summary>
        /// Gets bonus cash ins transfers for customer per campaign
        /// </summary>
        /// <returns><see cref="BonusCashInResponse"/></returns>
        [HttpGet("bonuses/customerId/{customerId}/campaignId/{campaignId}")]
        [ProducesResponseType(typeof(IEnumerable<BonusCashInResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IEnumerable<BonusCashInResponse>> GetBonusCashInsAsync([Required][FromRoute] string customerId, [Required][FromRoute] string campaignId)
        {
            var result = await _operationsQueryService.GetBonusCashInsAsync(customerId, campaignId);

            return _mapper.Map<IEnumerable<BonusCashInResponse>>(result);
        }

        /// <summary>
        /// Gets bonus cash ins transfers for customer per referral
        /// </summary>
        /// <returns><see cref="BonusCashInResponse"/></returns>
        [HttpGet("bonuses/customerId/{customerId}/referralId/{referralId}")]
        [ProducesResponseType(typeof(IEnumerable<BonusCashInResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IEnumerable<BonusCashInResponse>> GetBonusCashInsByReferralAsync([Required][FromRoute] string customerId, [Required][FromRoute] string referralId)
        {
            var result = await _operationsQueryService.GetBonusCashInsByReferralAsync(customerId, referralId);

            return _mapper.Map<IEnumerable<BonusCashInResponse>>(result);
        }
    }
}
