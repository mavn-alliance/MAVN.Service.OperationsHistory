using System.Threading.Tasks;
using Lykke.Common.MsSql;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.Domain.Repositories;
using Lykke.Service.OperationsHistory.MsSqlRepositories.Entities;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Repositories
{
    public class LinkWalletOperationsRepository : ILinkWalletOperationsRepository
    {
        private readonly MsSqlContextFactory<OperationsHistoryContext> _contextFactory;

        public LinkWalletOperationsRepository(MsSqlContextFactory<OperationsHistoryContext> contextFactory)
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
