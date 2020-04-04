using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Services;
using Lykke.Service.PrivateBlockchainFacade.Contract.Events;

namespace MAVN.Service.OperationsHistory.DomainServices.Subscribers
{
    public class FeeCollectedSubscriber : JsonRabbitSubscriber<FeeCollectedEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;

        public FeeCollectedSubscriber(
            IOperationsService operationsService,
            string connectionString,
            string exchangeName,
            string queueName,
            ILogFactory logFactory) : base(connectionString, exchangeName, queueName, logFactory)
        {
            _operationsService = operationsService;
            _log = logFactory.CreateLog(this);
        }


        protected override async Task ProcessMessageAsync(FeeCollectedEvent message)
        {
            var dto = new FeeCollectedOperationDto
            {
                CustomerId = message.CustomerId,
                Fee = message.Amount,
                OperationId = message.EventId,
                Reason = (FeeCollectionReason) message.Reason
            };

            await _operationsService.ProcessFeeCollectedAsync(dto);

            _log.Info("Processed FeeCollectedEvent", message);
        }
    }
}
