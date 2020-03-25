using System.Collections.Generic;

namespace Lykke.Service.OperationsHistory.Domain.Models
{
    public class PaginatedPartnersPaymentsHistory : BasePagedModel
    {
        public IEnumerable<IPartnersPayment> PartnersPaymentsHistory { get; set; }
    }
}
