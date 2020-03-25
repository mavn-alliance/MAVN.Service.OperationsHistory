using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.Domain.Services;
using Lykke.Service.WalletManagement.Contract.Events;

namespace Lykke.Service.OperationsHistory.DomainServices.Subscribers
{


    public class RefundPaymentTransferSubscriber : JsonRabbitSubscriber<RefundPaymentTransferEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;

        public RefundPaymentTransferSubscriber(
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

        protected override async Task ProcessMessageAsync(RefundPaymentTransferEvent message)
        {
            await _operationsService.ProcessRefundPaymentTransferEventAsync(new PaymentTransferDto
            {
                Amount = message.Amount,
                CustomerId = message.CustomerId,
                InvoiceId = message.InvoiceId,
                TransferId = message.TransferId,
                Timestamp = message.Timestamp,
                BurnRuleId = message.CampaignId,
                InstalmentName = message.InstalmentName,
                LocationCode = message.LocationCode,
            });

            _log.Info($"Processed {nameof(RefundPaymentTransferEvent)}", message);
        }
    }
}
