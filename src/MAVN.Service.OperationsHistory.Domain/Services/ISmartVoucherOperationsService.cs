using System.Threading.Tasks;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.Domain.Services
{
    public interface ISmartVoucherOperationsService
    {
        Task ProcessSmartVoucherSoldEventAsync(SmartVoucherPaymentDto smartVoucherPayment);
        Task ProcessSmartVoucherUsedEventAsync(SmartVoucherUseDto smartVoucherUse);
        Task ProcessSmartVoucherTransferredEventAsync(SmartVoucherTransferDto smartVoucherTransfer);
    }
}
