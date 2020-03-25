using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.CrossChainWalletLinker.Contract.Linking;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.Domain.Services;

namespace Lykke.Service.OperationsHistory.DomainServices.Subscribers
{
    public class WalletLinkingStateChangeCompletedSubscriber : JsonRabbitSubscriber<WalletLinkingStatusChangeCompletedEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;

        public WalletLinkingStateChangeCompletedSubscriber(
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

        protected override async Task ProcessMessageAsync(WalletLinkingStatusChangeCompletedEvent message)
        {
            await _operationsService.ProcessWalletLinkingStateChangeCompletedEventAsync(new LinkWalletOperationDto
            {
                CustomerId = message.CustomerId,
                Timestamp = DateTime.UtcNow,
                Fee = message.Fee,
                OperationId = message.EventId,
                PrivateAddress = message.PrivateAddress,
                PublicAddress = message.PublicAddress,
            });

            _log.Info($"Processed {nameof(WalletLinkingStatusChangeCompletedEvent)}", message);
        }
    }
}
