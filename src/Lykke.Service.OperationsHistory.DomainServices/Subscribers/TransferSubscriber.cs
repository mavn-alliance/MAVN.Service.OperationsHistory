using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.Domain.Services;
using Lykke.Service.WalletManagement.Contract.Events;

namespace Lykke.Service.OperationsHistory.DomainServices.Subscribers
{
    public class TransferSubscriber : JsonRabbitSubscriber<P2PTransferEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;

        public TransferSubscriber(
            string connectionString,
            string exchangeName,
            string queueName,
            IOperationsService operationsService,
            ILogFactory logFactory) : base(connectionString, exchangeName, queueName, true, logFactory)
        {
            _operationsService = operationsService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(P2PTransferEvent msg)
        {
            var transfer = new Transfer
            {
                AssetSymbol = msg.AssetSymbol,
                TransactionId = msg.TransactionId,
                Timestamp = msg.Timestamp,
                ReceiverCustomerId = msg.ReceiverCustomerId,
                SenderCustomerId = msg.SenderCustomerId,
                Amount = msg.Amount,
                ExternalOperationId = msg.ExternalOperationId
            };

            await _operationsService.ProcessTransferEventAsync(transfer);

            _log.Info($"Processed transfer event", msg);
        }
    }
}

