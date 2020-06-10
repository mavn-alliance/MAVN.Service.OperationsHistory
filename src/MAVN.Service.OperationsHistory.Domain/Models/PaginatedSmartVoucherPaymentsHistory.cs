using System.Collections;
using System.Collections.Generic;

namespace MAVN.Service.OperationsHistory.Domain.Models
{
    public class PaginatedSmartVoucherPaymentsHistory : BasePagedModel
    {
        public IEnumerable<SmartVoucherPaymentDto> SmartVoucherPaymentsHistory { get; set; }
    }
}
