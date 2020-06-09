using Autofac;
using JetBrains.Annotations;
using MAVN.Common.MsSql;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.MsSqlRepositories;
using MAVN.Service.OperationsHistory.MsSqlRepositories.Repositories;
using MAVN.Service.OperationsHistory.Settings;
using Lykke.SettingsReader;

namespace MAVN.Service.OperationsHistory.Modules
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

            builder.RegisterType<SmartVouchersRepository>()
                .As<ISmartVoucherRepository>()
                .SingleInstance();
        }
    }
}
