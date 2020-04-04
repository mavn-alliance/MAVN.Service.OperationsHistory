using System.Collections.Generic;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class PaginatedTransactionHistory : BasePagedModel
    {
        public IEnumerable<ITransactionHistory> TransactionsHistory { get; set; }
    }
}
