using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.OperationsHistory.Client
{
    /// <summary>
    /// OperationsHistory client settings.
    /// </summary>
    [PublicAPI]
    public class OperationsHistoryServiceClientSettings
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl { get; set; }
    }
}
