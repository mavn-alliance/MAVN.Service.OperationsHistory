using System;

namespace MAVN.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Represents a customer statistic response model
    /// </summary>
    public class CustomerStatisticResponse
    {
        /// <summary>
        /// The day the model represents
        /// </summary>
        public DateTime Day { get; set; }

        /// <summary>
        /// The total number of customers for the day
        /// </summary>
        public int Count { get; set; }
    }
}
