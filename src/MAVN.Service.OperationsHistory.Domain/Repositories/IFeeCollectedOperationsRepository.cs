using System.Threading.Tasks;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.Domain.Repositories
{
    public interface IFeeCollectedOperationsRepository
    {
        Task AddAsync(FeeCollectedOperationDto operation);
    }
}
