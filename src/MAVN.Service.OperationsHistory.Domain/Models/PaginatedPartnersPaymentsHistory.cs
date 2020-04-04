using System.Collections.Generic;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class PaginatedPartnersPaymentsHistory : BasePagedModel
    {
        public IEnumerable<IPartnersPayment> PartnersPaymentsHistory { get; set; }
    }
}
