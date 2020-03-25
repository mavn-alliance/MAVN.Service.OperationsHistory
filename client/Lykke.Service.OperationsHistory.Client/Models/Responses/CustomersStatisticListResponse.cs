using System.Collections.Generic;

namespace Lykke.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Represents a response model for a customers statistics list
    /// </summary>
    public class CustomersStatisticListResponse
    {
        /// <summary>
        /// Represents a list with active customers stats model
        /// </summary>
        public IReadOnlyList<CustomerStatisticResponse> ActiveCustomers { get; set; }

        /// <summary>
        /// Represents a total count active customers for the given period
        /// </summary>
        public int TotalActiveCustomers { get; set; }
    }
}
