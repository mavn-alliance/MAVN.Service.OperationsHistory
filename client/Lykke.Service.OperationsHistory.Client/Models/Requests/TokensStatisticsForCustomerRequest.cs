using System;
using System.ComponentModel.DataAnnotations;

namespace Lykke.Service.OperationsHistory.Client.Models.Requests
{
    /// <summary>
    /// Request model to get tokens statistics for a customer
    /// </summary>
    public class TokensStatisticsForCustomerRequest
    {
        /// <summary>
        /// Id of the customer
        /// </summary>
        [Required]
        public string CustomerId { get; set; }

        /// <summary>
        /// Inclusive start date for the period which you want to get statistics for, can be null
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Inclusive end date for the period which you want to get statistics for, can be null
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
