using System.Collections.Generic;

namespace Lykke.Service.OperationsHistory.Domain.Models
{
    public class PaginatedTransactionHistory : BasePagedModel
    {
        public IEnumerable<ITransactionHistory> TransactionsHistory { get; set; }
    }
}
