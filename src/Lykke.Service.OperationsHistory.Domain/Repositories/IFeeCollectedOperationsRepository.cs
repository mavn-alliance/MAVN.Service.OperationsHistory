using System.Threading.Tasks;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.Domain.Repositories
{
    public interface IFeeCollectedOperationsRepository
    {
        Task AddAsync(FeeCollectedOperationDto operation);
    }
}
