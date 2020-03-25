using System.Threading.Tasks;
using Lykke.Service.OperationsHistory.Domain.Models;

namespace Lykke.Service.OperationsHistory.Domain.Repositories
{
    public interface IReferralStakesRepository
    {
        Task AddReferralStakeAsync(ReferralStakeDto referralStake);

        Task AddReferralStakeReleasedAsync(ReferralStakeDto referralStake);
    }
}
