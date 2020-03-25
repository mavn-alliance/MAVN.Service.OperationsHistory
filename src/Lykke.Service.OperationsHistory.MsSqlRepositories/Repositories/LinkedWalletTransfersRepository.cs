using System.Threading.Tasks;
using Lykke.Common.MsSql;
using Lykke.Service.OperationsHistory.Domain.Models;
using Lykke.Service.OperationsHistory.Domain.Repositories;
using Lykke.Service.OperationsHistory.MsSqlRepositories.Entities;

namespace Lykke.Service.OperationsHistory.MsSqlRepositories.Repositories
{
    public class LinkedWalletTransfersRepository : ILinkedWalletTransfersRepository
    {
        private readonly MsSqlContextFactory<OperationsHistoryContext> _contextFactory;

        public LinkedWalletTransfersRepository(MsSqlContextFactory<OperationsHistoryContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddAsync(LinkedWalletTransferDto transfer)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var entity = LinkedWalletTransferEntity.Create(transfer);

                var historyEntity = TransactionHistoryEntity.CreateForLinkedWalletTransfer(transfer);
                
                context.LinkedWalletTransfers.Add(entity);
                
                context.TransactionHistories.Add(historyEntity);

                await context.SaveChangesAsync();
            }
        }
    }
}
