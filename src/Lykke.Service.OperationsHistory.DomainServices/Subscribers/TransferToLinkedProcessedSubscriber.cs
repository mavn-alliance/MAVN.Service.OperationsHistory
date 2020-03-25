using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.CrossChainTransfers.Contract;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.Domain.Services;

namespace Lykke.Service.OperationsHistory.DomainServices.Subscribers
{
    public class TransferToLinkedProcessedSubscriber : JsonRabbitSubscriber<TransferToExternalProcessedEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;
        
        public TransferToLinkedProcessedSubscriber(
            string connectionString,
            string exchangeName, 
            string queueName,
            ILogFactory logFactory, 
            IOperationsService operationsService) 
            : base(connectionString, exchangeName, queueName, logFactory)
        {
            _operationsService = operationsService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(TransferToExternalProcessedEvent message)
        {
            await _operationsService.ProcessTransferToLinkedEvent(new LinkedWalletTransferDto
            {
                Amount = message.Amount,
                CustomerId = message.CustomerId,
                PrivateAddress = message.PrivateAddress,
                PublicAddress = message.PublicAddress,
                OperationId = message.OperationId
            });
            
            _log.Info($"Processed {nameof(TransferToExternalProcessedEvent)}", message);
        }
    }
}
