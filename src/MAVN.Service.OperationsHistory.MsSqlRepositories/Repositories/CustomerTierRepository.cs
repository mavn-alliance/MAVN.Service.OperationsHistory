using System.Threading.Tasks;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.MsSqlRepositories.Entities;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Repositories
{
    public class CustomerTierRepository : ICustomerTierRepository
    {
        private readonly PostgreSQLContextFactory<OperationsHistoryContext> _contextFactory;

        public CustomerTierRepository(PostgreSQLContextFactory<OperationsHistoryContext> contextFactory)
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
