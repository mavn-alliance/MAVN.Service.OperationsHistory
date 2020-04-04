using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Gives the List of partners payments history on the desired page
    /// </summary>
    [PublicAPI]
    public class PaginatedPartnersPaymentsHistoryResponse : BasePagedResponse
    {
        /// <summary>
        /// List of parterns payments history
        /// </summary>
        public IEnumerable<PartnersPaymentResponse> PartnersPaymentsHistory { get; set; }
    }
}
