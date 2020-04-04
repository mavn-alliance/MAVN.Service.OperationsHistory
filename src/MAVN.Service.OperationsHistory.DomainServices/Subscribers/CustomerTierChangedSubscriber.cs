using System;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Services;
using Lykke.Service.Tiers.Contract;

namespace MAVN.Service.OperationsHistory.DomainServices.Subscribers
{
    public class CustomerTierChangedSubscriber : JsonRabbitSubscriber<CustomerTierChangedEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;

        public CustomerTierChangedSubscriber(
            string connectionString,
            string exchangeName,
            string queueName,
            IOperationsService operationsService,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, queueName, true, logFactory)
        {
            _operationsService = operationsService;
            _log = logFactory.CreateLog(this);
        }

        protected override async Task ProcessMessageAsync(CustomerTierChangedEvent message)
        {
            var model = new CustomerTierModel
            {
                Id = Guid.NewGuid().ToString(),
                CustomerId = message.CustomerId.ToString(),
                TierId = message.TierId.ToString(),
                Timestamp = DateTime.UtcNow
            };

            await _operationsService.ProcessCustomerTierChangedEventAsync(model);

            _log.Info("Processed customer tier changed event",
                context: $"customerId: {message.CustomerId}; tierId: {message.TierId}");
        }
    }
}
