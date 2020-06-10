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
    public class SmartVoucherSoldSubscriber : JsonRabbitSubscriber<SmartVoucherSoldEvent>
    {
        private readonly ISmartVoucherOperationsService _operationsService;
        private readonly ILog _log;

        public SmartVoucherSoldSubscriber(
            string connectionString,
            string exchangeName,
            string queueName,
            ISmartVoucherOperationsService operationsService,
            ILogFactory logFactory) : base(connectionString, exchangeName, queueName, logFactory)
        {
            _operationsService = operationsService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(SmartVoucherSoldEvent message)
        {
            var dto = new SmartVoucherPaymentDto
            {
                Amount = message.Amount,
                AssetSymbol = message.Currency,
                CustomerId = message.CustomerId,
                PartnerId = message.PartnerId,
                ShortCode = message.VoucherShortCode,
                Timestamp = message.Timestamp,
                CampaignId = message.CampaignId.ToString(),
                PaymentRequestId = string.IsNullOrEmpty(message.PaymentRequestId)
                    ? Guid.NewGuid().ToString()
                    : message.PaymentRequestId,
            };

            await _operationsService.ProcessSmartVoucherSoldEventAsync(dto);
            _log.Info("Processed SmartVoucherSoldEvent", context:message);
        }
    }
}
