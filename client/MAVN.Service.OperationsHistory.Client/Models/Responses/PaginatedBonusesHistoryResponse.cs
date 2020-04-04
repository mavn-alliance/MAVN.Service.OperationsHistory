using System.Collections.Generic;
using JetBrains.Annotations;

namespace MAVN.Service.OperationsHistory.Client.Models.Responses
{
    /// <summary>
    /// Gives the List of bonuses history on the desired page
    /// </summary>
    [PublicAPI]
    public class PaginatedBonusesHistoryResponse : BasePagedResponse
    {
        /// <summary>
        /// List of bonuses history
        /// </summary>
        public IEnumerable<BonusCashInResponse> BonusesHistory { get; set; }
    }
}
