using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Services;
using MAVN.Service.Staking.Contract.Events;

namespace MAVN.Service.OperationsHistory.DomainServices.Subscribers
{
    public class ReferralStakeTokensReservationSubscriber : JsonRabbitSubscriber<ReferralStakeReservedEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;

        public ReferralStakeTokensReservationSubscriber(
            string connectionString,
            string exchangeName,
            string queueName,
            IOperationsService operationsService,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, queueName, logFactory)
        {
            _operationsService = operationsService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(ReferralStakeReservedEvent message)
        {
            var referralStakeDto = new ReferralStakeDto
            {
                Amount = message.Amount,
                CampaignId = message.CampaignId,
                CustomerId = message.CustomerId,
                ReferralId = message.ReferralId,
                Timestamp = message.Timestamp
            };

            await _operationsService.ProcessReferralStakeTokensReservationEventAsync(referralStakeDto);

            _log.Info("Processed ReferralStakeReservedEvent", message);
        }
    }
}
