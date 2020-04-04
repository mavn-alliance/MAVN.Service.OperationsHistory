using System.Collections.Generic;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class PaginatedBonusesHistory : BasePagedModel
    {
        public IEnumerable<IBonusCashIn> BonusesHistory { get; set; }
    }
}
