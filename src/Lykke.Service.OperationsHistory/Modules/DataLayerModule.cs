using Autofac;
using JetBrains.Annotations;
using Lykke.Common.MsSql;
using Lykke.Service.OperationsHistory.Domain.Repositories;
using Lykke.Service.OperationsHistory.MsSqlRepositories;
using Lykke.Service.OperationsHistory.MsSqlRepositories.Repositories;
using Lykke.Service.OperationsHistory.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.OperationsHistory.Modules
{
    [UsedImplicitly]
    public class DataLayerModule : Module
    {
        private readonly string _connectionString;

        public DataLayerModule(IReloadingManager<AppSettings> appSettings)
        {
            _connectionString = appSettings.CurrentValue.OperationsHistoryService.Db.DataConnString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMsSql(
                _connectionString,
                connString => new OperationsHistoryContext(connString, false),
                dbConn => new OperationsHistoryContext(dbConn));

            builder.RegisterType<TransfersRepository>()
                .As<ITransfersRepository>()
                .SingleInstance();

            builder.RegisterType<BonusCashInsRepository>()
                .As<IBonusCashInsRepository>()
                .SingleInstance();

            builder.RegisterType<TransactionHistoryRepository>()
                .As<ITransactionHistoryRepository>()
                .SingleInstance();

            builder.RegisterType<PaymentTransfersRepository>()
                .As<IPaymentTransfersRepository>()
                .SingleInstance();

            builder.RegisterType<CustomerTierRepository>()
                .As<ICustomerTierRepository>()
                .SingleInstance();

            builder.RegisterType<PartnersPaymentsRepository>()
                .As<IPartnersPaymentsRepository>()
                .SingleInstance();

            builder.RegisterType<ReferralStakesRepository>()
                .As<IReferralStakesRepository>()
                .SingleInstance();

            builder.RegisterType<LinkedWalletTransfersRepository>()
                .As<ILinkedWalletTransfersRepository>()
                .SingleInstance();

            builder.RegisterType<FeeCollectedOperationsRepository>()
                .As<IFeeCollectedOperationsRepository>()
                .SingleInstance();

            builder.RegisterType<LinkWalletOperationsRepository>()
                .As<ILinkWalletOperationsRepository>()
                .SingleInstance();

            builder.RegisterType<VoucherPurchasePaymentsRepository>()
                .As<IVoucherPurchasePaymentsRepository>()
                .SingleInstance();
        }
    }
}
