using System;

namespace Lykke.Service.OperationsHistory.Client.Models.Requests
{
    /// <summary>
    /// Represents a base period request model
    /// </summary>
    public class PeriodRequest
    {
        /// <summary>
        ///  Represents FromDate 
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        ///  Represents ToDate
        /// </summary>
        public DateTime ToDate { get; set; }
    }
}
