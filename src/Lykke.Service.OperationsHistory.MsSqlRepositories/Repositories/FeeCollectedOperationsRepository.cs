using System.Threading.Tasks;
using Lykke.Common.MsSql;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.Domain.Repositories;
using Lykke.Service.OperationsHistory.MsSqlRepositories.Entities;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Repositories
{
    public class FeeCollectedOperationsRepository : IFeeCollectedOperationsRepository
    {
        private readonly MsSqlContextFactory<OperationsHistoryContext> _contextFactory;

        public FeeCollectedOperationsRepository(MsSqlContextFactory<OperationsHistoryContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddAsync(FeeCollectedOperationDto operation)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = FeeCollectedOperationEntity.Create(operation);

                var historyEntity = TransactionHistoryEntity.Create(operation);

                context.FeeCollectedOperations.Add(entity);

                context.TransactionHistories.Add(historyEntity);

                await context.SaveChangesAsync();
            }
        }
    }
}
