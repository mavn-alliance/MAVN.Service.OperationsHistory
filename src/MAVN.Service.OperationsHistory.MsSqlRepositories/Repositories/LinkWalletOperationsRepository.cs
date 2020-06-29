using System.Threading.Tasks;
using MAVN.Persistence.PostgreSQL.Legacy;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.MsSqlRepositories.Entities;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Repositories
{
    public class LinkWalletOperationsRepository : ILinkWalletOperationsRepository
    {
        private readonly PostgreSQLContextFactory<OperationsHistoryContext> _contextFactory;

        public LinkWalletOperationsRepository(PostgreSQLContextFactory<OperationsHistoryContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }
        
        public async Task AddAsync(LinkWalletOperationDto operation)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = LinkWalletOperationEntity.Create(operation);

                context.LinkWalletOperations.Add(entity);
                
                await context.SaveChangesAsync();
            }
        }
    }
}
