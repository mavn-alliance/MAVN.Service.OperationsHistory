using System.Collections.Generic;

namespace Lykke.Service.OperationsHistory.Domain.Models
{
    public class CustomersStatisticListModel
    {
        public IReadOnlyList<CustomerStatisticModel> ActiveCustomers { get; set; }

        public int TotalActiveCustomers { get; set; }
    }
}
