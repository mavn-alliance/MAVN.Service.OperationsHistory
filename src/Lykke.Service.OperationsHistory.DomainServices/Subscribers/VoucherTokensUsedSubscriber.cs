using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Subscriber;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.Domain.Services;
using Lykke.Service.Vouchers.Contract;

namespace Lykke.Service.OperationsHistory.DomainServices.Subscribers
{
    public class VoucherTokensUsedSubscriber : JsonRabbitSubscriber<VoucherTokensUsedEvent>
    {
        private readonly IOperationsService _operationsService;
        private readonly ILog _log;

        public VoucherTokensUsedSubscriber(
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

        protected override async Task ProcessMessageAsync(VoucherTokensUsedEvent message)
        {
            await _operationsService.ProcessVoucherTokensUsedEventAsync(new VoucherPurchasePaymentDto
            {
                TransferId = message.TransferId,
                CustomerId = message.CustomerId,
                SpendRuleId = message.SpendRuleId,
                VoucherId = message.VoucherId,
                Amount = message.Amount,
                Timestamp = message.Timestamp,
            });

            _log.Info("Processed successful voucher tokens reserved event", message);
        }
    }
}
