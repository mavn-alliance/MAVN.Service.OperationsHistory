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
    public class SmartVoucherTransferredSubscriber : JsonRabbitSubscriber<SmartVoucherTransferredEvent>
    {
        private readonly ISmartVoucherOperationsService _operationsService;
        private readonly ILog _log;

        public SmartVoucherTransferredSubscriber(
            string connectionString,
            string exchangeName,
            string queueName,
            ISmartVoucherOperationsService operationsService,
            ILogFactory logFactory) : base(connectionString, exchangeName, queueName, logFactory)
        {
            _operationsService = operationsService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(SmartVoucherTransferredEvent message)
        {
            var dto = new SmartVoucherTransferDto
            {
                Amount = message.Amount,
                AssetSymbol = message.Currency,
                PartnerId = message.PartnerId,
                ShortCode = message.VoucherShortCode,
                Timestamp = message.Timestamp,
                CampaignId = message.CampaignId.ToString(),
                NewCustomerId = message.NewCustomerId,
                OldCustomerId = message.OldCustomerId,
            };

            await _operationsService.ProcessSmartVoucherTransferredEventAsync(dto);
            _log.Info("Processed SmartVoucherTransferredEvent", context: message);
        }
    }
}
