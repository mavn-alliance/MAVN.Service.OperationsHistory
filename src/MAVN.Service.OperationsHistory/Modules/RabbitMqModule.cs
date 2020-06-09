using Autofac;
using JetBrains.Annotations;
using Lykke.Common;
using MAVN.Service.OperationsHistory.DomainServices.Subscribers;
using MAVN.Service.OperationsHistory.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.OperationsHistory.Modules
{
    [UsedImplicitly]
    public class RabbitMqModule : Module
    {
        private const string TransferToInternalCompletedExchangeName = "lykke.wallet.transfertointernalcompleted";
        private const string TransferToExternalProcessedExchangeName = "lykke.wallet.transfertoexternalprocessed";
        private const string WalletLinkingStatusChangeCompletedExchangeName = "lykke.wallet.walletlinkingstatuschangecompleted";

        private const string DefaultQueueName = "operationshistory";
        private const string PartnersPaymentTokensReservedExchange = "lykke.wallet.partnerspaymenttokensreserved";
        private const string RefundPartnersPaymentExchange = "lykke.wallet.refundpartnerspayment";
        private const string BonusReceivedExchangeName = "lykke.wallet.bonusreceived";
        private const string CustomerTierChangedExchangeName = "lykke.bonus.customertierchanged";
        private const string TransferExchangeName = "lykke.wallet.transfer";
        private const string ReferralStakeReservedExchange = "lykke.wallet.referralstakereserved";
        private const string ReferralStakeReleasedExchange = "lykke.wallet.referralstakereleased";
        private const string FeeCollectedExchangeName = "lykke.wallet.feecollected";
        private const string VoucherTokensUsedExchangeName = "lykke.wallet.vouchertokensused";
        private const string SmartVoucherSoldExchangeName = "lykke.smart-vouchers.vouchersold";
        private const string SmartVoucherUsedExchangeName = "lykke.smart-vouchers.voucherused";

        private readonly string _connString;
        private readonly bool _isPublicBlockchainFeatureDisabled;

        public RabbitMqModule(IReloadingManager<AppSettings> reloadingManager)
        {
            var appSettings = reloadingManager.CurrentValue;
            _connString = appSettings.OperationsHistoryService.RabbitMq.RabbitMqConnectionString;
            _isPublicBlockchainFeatureDisabled = appSettings.OperationsHistoryService.IsPublicBlockchainFeatureDisabled
                ?? false;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TransferSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", TransferExchangeName)
                .WithParameter("queueName", DefaultQueueName);

            builder.RegisterType<BonusReceivedSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", BonusReceivedExchangeName)
                .WithParameter("queueName", DefaultQueueName);

            builder.RegisterType<CustomerTierChangedSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", CustomerTierChangedExchangeName)
                .WithParameter("queueName", DefaultQueueName);

            builder.RegisterType<RefundPartnersPaymentSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", RefundPartnersPaymentExchange)
                .WithParameter("queueName", DefaultQueueName)
                .SingleInstance();

            builder.RegisterType<PartnersPaymentTokensReservedSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", PartnersPaymentTokensReservedExchange)
                .WithParameter("queueName", DefaultQueueName)
                .SingleInstance();

            builder.RegisterType<ReferralStakeTokensReservationSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", ReferralStakeReservedExchange)
                .WithParameter("queueName", DefaultQueueName)
                .SingleInstance();

            builder.RegisterType<ReferralStakeTokensReleasedSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", ReferralStakeReleasedExchange)
                .WithParameter("queueName", DefaultQueueName)
                .SingleInstance();

            builder.RegisterType<FeeCollectedSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", FeeCollectedExchangeName)
                .WithParameter("queueName", DefaultQueueName)
                .SingleInstance();

            builder.RegisterType<VoucherTokensUsedSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", VoucherTokensUsedExchangeName)
                .WithParameter("queueName", DefaultQueueName)
                .SingleInstance();

            builder.RegisterType<SmartVoucherSoldSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", SmartVoucherSoldExchangeName)
                .WithParameter("queueName", DefaultQueueName)
                .SingleInstance();

            builder.RegisterType<SmartVoucherUsedSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", SmartVoucherUsedExchangeName)
                .WithParameter("queueName", DefaultQueueName)
                .SingleInstance();

            if (!_isPublicBlockchainFeatureDisabled)
                RegisterSubscribersForPublicBlockchainFeature(builder);
        }

        private void RegisterSubscribersForPublicBlockchainFeature(ContainerBuilder builder)
        {
            builder.RegisterType<TransferToLinkedProcessedSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", TransferToExternalProcessedExchangeName)
                .WithParameter("queueName", DefaultQueueName)
                .SingleInstance();

            builder.RegisterType<TransferToInternalCompletedSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", TransferToInternalCompletedExchangeName)
                .WithParameter("queueName", DefaultQueueName)
                .SingleInstance();

            builder.RegisterType<WalletLinkingStateChangeCompletedSubscriber>()
                .As<IStartStop>()
                .WithParameter("connectionString", _connString)
                .WithParameter("exchangeName", WalletLinkingStatusChangeCompletedExchangeName)
                .WithParameter("queueName", DefaultQueueName)
                .SingleInstance();
        }
    }
}
