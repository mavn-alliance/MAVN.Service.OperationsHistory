using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Services;
using MAVN.Service.SmartVouchers.Contract;

namespace MAVN.Service.OperationsHistory.DomainServices.Subscribers
{
    public class SmartVoucherUsedSubscriber : JsonRabbitSubscriber<SmartVoucherUsedEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;

        public SmartVoucherUsedSubscriber(
            string connectionString,
            string exchangeName,
            string queueName,
            IOperationsService operationsService,
            ILogFactory logFactory) : base(connectionString, exchangeName, queueName, logFactory)
        {
            _operationsService = operationsService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(SmartVoucherUsedEvent message)
        {
            var dto = new SmartVoucherUseDto
            {
                Amount = message.Amount,
                AssetSymbol = message.Currency,
                CustomerId = message.CustomerId,
                PartnerId = message.PartnerId,
                Timestamp = message.Timestamp,
                CampaignId = message.CampaignId,
                LinkedCustomerId = message.LinkedCustomerId,
                LocationId = message.LocationId,
            };

            await _operationsService.ProcessSmartVoucherUsedEventAsync(dto);
            _log.Info("Processed SmartVoucherUsedEvent", context: message);
        }
    }
}
