using System;

namespace MAVN.Service.OperationsHistory.Client.Models.Requests
{
    /// <summary>
    /// Contains the current page, the amount of items on each page and dates range
    /// </summary>
    public class PaginationModelWithDatesRange : PaginationModel
    {
        /// <summary>Represents FromDate </summary>
        public DateTime FromDate { get; set; }

        /// <summary>Represents ToDate</summary>
        public DateTime ToDate { get; set; }
    }
}
