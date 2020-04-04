using Autofac;
using Lykke.Sdk;
using Lykke.Service.Campaign.Client;
using Lykke.Service.CustomerProfile.Client;
using MAVN.Service.OperationsHistory.Domain.Services;
using MAVN.Service.OperationsHistory.DomainServices.Services;
using MAVN.Service.OperationsHistory.Services;
using MAVN.Service.OperationsHistory.Settings;
using Lykke.Service.PartnerManagement.Client;
using Lykke.Service.PrivateBlockchainFacade.Client;
using Lykke.SettingsReader;

namespace MAVN.Service.OperationsHistory.Modules
{
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StartupManager>()
                .As<IStartupManager>()
                .SingleInstance();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>()
                .AutoActivate()
                .SingleInstance();

            builder.RegisterType<OperationsService>()
                .WithParameter(TypedParameter.From(_appSettings.CurrentValue.OperationsHistoryService.CustomerWalletCacheExpirationPeriod))
                .WithParameter(TypedParameter.From(_appSettings.CurrentValue.Constants.TokenSymbol))
                .As<IOperationsService>()
                .SingleInstance();

            builder.RegisterType<OperationsQueryService>()
                .As<IOperationsQueryService>()
                .SingleInstance();

            builder.RegisterType<StatisticsService>()
                .As<IStatisticsService>()
                .WithParameter(TypedParameter.From(_appSettings.CurrentValue.Constants.TokenSymbol))
                .SingleInstance();

            builder.RegisterCampaignClient(_appSettings.CurrentValue.CampaignService, null);
            
            builder.RegisterPrivateBlockchainFacadeClient(_appSettings.CurrentValue.PrivateBlockchainFacadeClient, null);

            builder.RegisterCustomerProfileClient(_appSettings.CurrentValue.CustomerProfileService);

            builder.RegisterPartnerManagementClient(_appSettings.CurrentValue.PartnerManagementService, null);
        }
    }
}
