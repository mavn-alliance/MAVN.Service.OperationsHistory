using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.Domain.Services;
using Lykke.Service.WalletManagement.Contract.Events;

namespace Lykke.Service.OperationsHistory.DomainServices.Subscribers
{
    public class BonusReceivedSubscriber : JsonRabbitSubscriber<BonusReceivedEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;

        public BonusReceivedSubscriber(
            string connectionString,
            string exchangeName,
            string queueName,
            IOperationsService operationsService,
            ILogFactory logFactory) : base(connectionString, exchangeName, queueName, true, logFactory)
        {
            _operationsService = operationsService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(BonusReceivedEvent msg)
        {
            var bonusCashIn = new BonusCashInDto
            {
                AssetSymbol = msg.AssetSymbol,
                TransactionId = msg.TransactionId,
                Timestamp = msg.Timestamp,
                ExternalOperationId = msg.ExternalOperationId,
                CustomerId = msg.CustomerId,
                LocationCode = msg.LocationCode,
                BonusType = msg.BonusType,
                Amount = msg.Amount,
                CampaignId = msg.CampaignId.ToString(),
                PartnerId = msg.PartnerId,
                LocationId = msg.LocationId,
                ConditionId = msg.ConditionId != Guid.Empty ? msg.ConditionId.ToString() : null,
                ReferralId = !string.IsNullOrWhiteSpace(msg.ReferralId) && msg.ReferralId != Guid.Empty.ToString()
                    ? msg.ReferralId : null
            };

            await _operationsService.ProcessBonusReceivedEventAsync(bonusCashIn);

            _log.Info($"Processed bonus cash in event", msg);
        }
    }
}
