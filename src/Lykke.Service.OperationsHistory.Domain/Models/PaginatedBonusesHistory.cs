using System.Collections.Generic;

namespace Lykke.Service.OperationsHistory.Domain.Models
{
    public class PaginatedBonusesHistory : BasePagedModel
    {
        public IEnumerable<IBonusCashIn> BonusesHistory { get; set; }
    }
}
