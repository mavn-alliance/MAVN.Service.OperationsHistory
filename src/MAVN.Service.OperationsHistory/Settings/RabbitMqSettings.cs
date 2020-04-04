using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.OperationsHistory.Settings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string RabbitMqConnectionString { get; set; }
    }
}
