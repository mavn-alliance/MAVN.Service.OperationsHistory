using System;
using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.OperationsHistory.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class OperationsHistorySettings
    {
        public DbSettings Db { get; set; }

        public RabbitMqSettings RabbitMq { get; set; }

        public TimeSpan CustomerWalletCacheExpirationPeriod { get; set; }

        [Optional]
        public bool? IsPublicBlockchainFeatureDisabled { get; set; }
    }
}
