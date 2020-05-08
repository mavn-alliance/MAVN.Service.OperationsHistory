using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Services;
using MAVN.Service.PartnersPayments.Contract;

namespace MAVN.Service.OperationsHistory.DomainServices.Subscribers
{
    public class PartnersPaymentTokensReservedSubscriber : JsonRabbitSubscriber<PartnersPaymentTokensReservedEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;

        public PartnersPaymentTokensReservedSubscriber(
            IOperationsService operationsService,
            string connectionString,
            string exchangeName,
            string queueName,
            ILogFactory logFactory) : base(connectionString, exchangeName, queueName, logFactory)
        {
            _operationsService = operationsService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(PartnersPaymentTokensReservedEvent message)
        {
            await _operationsService.ProcessPartnersPaymentTokensReservedEventAsync(new PartnerPaymentDto
            {
                Amount = message.Amount,
                CustomerId = message.CustomerId,
                Timestamp = message.Timestamp,
                PartnerId = message.PartnerId,
                PaymentRequestId = message.PaymentRequestId,
                LocationId = message.LocationId
            });

            _log.Info("Processed PartnersPaymentTokensReservedEvent", message);
        }
    }
}
