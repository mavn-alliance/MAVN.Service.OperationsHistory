using System;
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
    [Route("api/statistics")]
    public class StatisticsController : Controller, IStatisticsApi
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IMapper _mapper;

        public StatisticsController(IStatisticsService statisticsService, IMapper mapper)
        {
            _statisticsService = statisticsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the count of active customers between two dates
        /// </summary>
        /// <returns><see cref="ActiveCustomersCountResponse"/></returns>
        [HttpGet("customers/active")]
        [ProducesResponseType(typeof(ActiveCustomersCountResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActiveCustomersCountResponse> GetActiveCustomersCountAsync
            ([Required] [FromQuery] DateTime fromDate, [Required] [FromQuery] DateTime toDate)
        {
            var result = await _statisticsService
                .GetActiveCustomersCountAsync(fromDate, toDate);

            return new ActiveCustomersCountResponse { ActiveCustomersCount = result };
        }

        /// <summary>
        /// Gets statistics for tokens in the system
        /// </summary>
        /// <param name="dateFrom">The inclusive start date for fetching data</param>
        /// <param name="dateTo">The exclusive end date for fetching data</param>
        /// <returns></returns>
        [HttpGet("tokens")]
        [ProducesResponseType(typeof(IEnumerable<TokensAmountResponseModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IEnumerable<TokensAmountResponseModel>> GetTokensStatisticsAsync
            ([Required] [FromQuery] DateTime dateFrom, [Required] [FromQuery] DateTime dateTo)
        {
            var result = await _statisticsService.GetTokensStatisticsAsync(dateFrom, dateTo);

            return _mapper.Map<IEnumerable<TokensAmountResponseModel>>(result);
        }

        /// <summary>
        /// Get a customers statistics model
        /// </summary>
        /// <param name="periodRequest"><see cref="PeriodRequest"/></param>
        /// <returns><see cref="CustomersStatisticListResponse"/></returns>
        [HttpGet("customers")]
        [ProducesResponseType(typeof(CustomersStatisticListResponse), (int)HttpStatusCode.OK)]
        public async Task<CustomersStatisticListResponse> GetCustomerStatisticsAsync(PeriodRequest periodRequest)
        {
            var result = await _statisticsService.GetCustomersStatisticsByDayAsync(periodRequest.FromDate, periodRequest.ToDate);

            return _mapper.Map<CustomersStatisticListResponse>(result); 
        }

        /// <summary>
        /// Gets statistics for tokens for a customer in the system
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("customers/tokens")]
        [ProducesResponseType(typeof(TokensAmountResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<TokensAmountResponseModel> GetTokensStatisticsForCustomerAsync
            ([FromQuery] TokensStatisticsForCustomerRequest request)
        {
            if (request.StartDate > request.EndDate)
                throw new BadRequestException($"{nameof(request.StartDate)} must be earlier than {nameof(request.EndDate)}");

            var result =
                await _statisticsService.GetTokensStatisticsForCustomerAsync(request.CustomerId, request.StartDate,
                    request.EndDate);

            return _mapper.Map<TokensAmountResponseModel>(result);
        }
    }
}
