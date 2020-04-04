using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Services;
using Lykke.Service.PaymentTransfers.Contract;

namespace MAVN.Service.OperationsHistory.DomainServices.Subscribers
{
    public class PaymentTransferTokensReservedSubscriber : JsonRabbitSubscriber<PaymentTransferTokensReservedEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;

        public PaymentTransferTokensReservedSubscriber(
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


        protected override async Task ProcessMessageAsync(PaymentTransferTokensReservedEvent message)
        {
            await _operationsService.ProcessPaymentTransferTokensReservedEventAsync(new PaymentTransferDto
            {
                Amount = message.Amount,
                BurnRuleId = message.CampaignId,
                CustomerId = message.CustomerId,
                InvoiceId = message.InvoiceId,
                TransferId = message.TransferId,
                Timestamp = message.Timestamp,
                InstalmentName = message.InstalmentName,
                LocationCode = message.LocationCode,
            });

            _log.Info("Processed successful payment transfer event", message);
        }
    }
}
