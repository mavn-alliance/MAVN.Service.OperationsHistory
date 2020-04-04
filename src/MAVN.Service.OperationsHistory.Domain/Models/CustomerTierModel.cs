using System;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    /// <summary>
    /// Represents the reward tier associated with customer.
    /// </summary>
    public class CustomerTierModel
    {
        /// <summary>
        /// The item identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The customer identifier.
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// The reward tier identifier.
        /// </summary>
        public string TierId { get; set; }

        /// <summary>
        /// The date and timer of customer tier changed event.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}
