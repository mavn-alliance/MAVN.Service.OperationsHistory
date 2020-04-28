using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Services;
using MAVN.Service.PaymentManagement.Contract;

namespace MAVN.Service.OperationsHistory.DomainServices.Subscribers
{
    public class SmartVoucherPaymentCompletedSubscriber : JsonRabbitSubscriber<PaymentCompletedEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;

        public SmartVoucherPaymentCompletedSubscriber(
            string connectionString,
            string exchangeName,
            string queueName,
            IOperationsService operationsService,
            ILogFactory logFactory) : base(connectionString, exchangeName, queueName, logFactory)
        {
            _operationsService = operationsService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(PaymentCompletedEvent message)
        {
            var dto = new SmartVoucherPaymentDto
            {
                Amount = message.Amount,
                AssetSymbol = message.Currency,
                CustomerId = message.CustomerId,
                PartnerId = message.PartnerId,
                PaymentRequestId = message.PaymentRequestId,
                Timestamp = message.Timestamp
            };

            await _operationsService.ProcessSmartVoucherPaymentCompletedEventAsync(dto);
            _log.Info("Processed PaymentCompletedEvent", context:message);
        }
    }
}
