using System.Threading.Tasks;
using Lykke.Common.MsSql;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.Domain.Repositories;
using Lykke.Service.OperationsHistory.MsSqlRepositories.Entities;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Repositories
{
    public class CustomerTierRepository : ICustomerTierRepository
    {
        private readonly MsSqlContextFactory<OperationsHistoryContext> _contextFactory;

        public CustomerTierRepository(MsSqlContextFactory<OperationsHistoryContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task InsertAsync(CustomerTierModel customerTier)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = new CustomerTierEntity(customerTier);

                context.CustomerTiers.Add(entity);

                await context.SaveChangesAsync();
            }
        }
    }
}
