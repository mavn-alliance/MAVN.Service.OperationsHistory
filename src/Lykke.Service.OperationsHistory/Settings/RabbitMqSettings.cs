using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.OperationsHistory.Settings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string RabbitMqConnectionString { get; set; }
    }
}
