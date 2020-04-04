namespace MAVN.Service.OperationsHistory.Client.Models
{
    /// <summary>
    /// Base model for paginated results
    /// </summary>
    public class BasePagedResponse
    {
        /// <summary>
        /// Total count of the items
        /// </summary>
        public int TotalCount { get; set; }
    }
}
