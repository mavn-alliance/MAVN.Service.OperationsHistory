using Lykke.HttpClientGenerator;

namespace MAVN.Service.OperationsHistory.Client
{
    /// <inheritdoc/>
    public class OperationsHistoryClient : IOperationsHistoryClient
    {
        /// <summary>
        /// Initializes a new instance of <see cref="OperationsHistoryClient"/> with <param name="httpClientGenerator"></param>.
        /// </summary> 
        public OperationsHistoryClient(IHttpClientGenerator httpClientGenerator)
        {
            OperationsHistoryApi = httpClientGenerator.Generate<IOperationsHistoryApi>();
            StatisticsApi = httpClientGenerator.Generate<IStatisticsApi>();
        }

        /// <inheritdoc/>
        public IOperationsHistoryApi OperationsHistoryApi { get; }

        /// <inheritdoc/>
        public IStatisticsApi StatisticsApi { get; }
    }
}
