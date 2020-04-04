using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.OperationsHistory.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }

        public string DataConnString { get; set; }

    }
}
