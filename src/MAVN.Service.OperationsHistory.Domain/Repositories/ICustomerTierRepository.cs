using System.Threading.Tasks;
using MAVN.Service.OperationsHistory.Domain.Models;

namespace MAVN.Service.OperationsHistory.Domain.Repositories
{
    public interface ICustomerTierRepository
    {
        Task InsertAsync(CustomerTierModel customerTier);
    }
}
