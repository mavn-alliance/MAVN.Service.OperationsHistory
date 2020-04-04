using JetBrains.Annotations;

namespace MAVN.Service.OperationsHistory.Client
{
    /// <summary>
    /// Operations history service client.
    /// </summary>
    [PublicAPI]
    public interface IOperationsHistoryClient
    {
        /// <summary>
        /// Operations history API.
        /// </summary>
        IOperationsHistoryApi OperationsHistoryApi { get; }

        /// <summary>
        /// Statistics API.
        /// </summary>
        IStatisticsApi StatisticsApi { get; }
    }
}
