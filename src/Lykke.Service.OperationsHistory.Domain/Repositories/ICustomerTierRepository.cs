using System.Threading.Tasks;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.Domain.Repositories
{
    public interface ICustomerTierRepository
    {
        Task InsertAsync(CustomerTierModel customerTier);
    }
}
