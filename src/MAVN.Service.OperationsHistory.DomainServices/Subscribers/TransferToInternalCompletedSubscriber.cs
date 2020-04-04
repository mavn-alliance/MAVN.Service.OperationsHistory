using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.CrossChainTransfers.Contract;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Services;

namespace MAVN.Service.OperationsHistory.DomainServices.Subscribers
{
    public class TransferToInternalCompletedSubscriber : JsonRabbitSubscriber<TransferToInternalCompletedEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;
        
        public TransferToInternalCompletedSubscriber(
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

        protected override async Task ProcessMessageAsync(TransferToInternalCompletedEvent message)
        {
            await _operationsService.ProcessTransferFromLinkedToInternalEvent(new LinkedWalletTransferDto
            {
                Amount = message.Amount,
                CustomerId = message.CustomerId,
                PrivateAddress = message.PrivateAddress,
                PublicAddress = message.PublicAddress,
                OperationId = message.OperationId,
            });
            
            _log.Info($"Processed {nameof(TransferToInternalCompletedEvent)}", message);
        }
    }
}
