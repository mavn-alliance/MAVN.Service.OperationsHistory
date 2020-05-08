using JetBrains.Annotations;
using Lykke.Sdk.Settings;
using MAVN.Service.Campaign.Client;
using MAVN.Service.CustomerProfile.Client;
using MAVN.Service.PartnerManagement.Client;
using MAVN.Service.PrivateBlockchainFacade.Client;

namespace MAVN.Service.OperationsHistory.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public OperationsHistorySettings OperationsHistoryService { get; set; }

        public CampaignServiceClientSettings CampaignService { get; set; }

        public PrivateBlockchainFacadeServiceClientSettings PrivateBlockchainFacadeClient { get; set; }

        public CustomerProfileServiceClientSettings CustomerProfileService { get; set; }

        public PartnerManagementServiceClientSettings PartnerManagementService { get; set; }

        public Constants Constants { get; set; }
    }
}
