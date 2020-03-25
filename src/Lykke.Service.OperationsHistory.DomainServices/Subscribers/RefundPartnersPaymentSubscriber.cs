using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.Domain.Services;
using Lykke.Service.WalletManagement.Contract.Events;

namespace Lykke.Service.OperationsHistory.DomainServices.Subscribers
{
    public class RefundPartnersPaymentSubscriber : JsonRabbitSubscriber<RefundPartnersPaymentEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;

        public RefundPartnersPaymentSubscriber(
            IOperationsService operationsService,
            string connectionString,
            string exchangeName, 
            string queueName,
            ILogFactory logFactory) 
            : base(connectionString, exchangeName, queueName, logFactory)
        {
            _operationsService = operationsService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(RefundPartnersPaymentEvent message)
        {
            await _operationsService.ProcessRefundPartnersPaymentEventAsync(new PartnerPaymentDto
            {
                Amount = message.Amount,
                CustomerId = message.CustomerId,
                Timestamp = message.Timestamp,
                PartnerId = message.PartnerId,
                PaymentRequestId = message.PaymentRequestId,
                LocationId = message.LocationId
            });

            _log.Info("Processed RefundPartnersPaymentEvent", message);
        }
    }
}
