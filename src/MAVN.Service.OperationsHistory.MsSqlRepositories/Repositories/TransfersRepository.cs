using System.Linq;
using System.Threading.Tasks;
using Lykke.Common.MsSql;
using MAVN.Service.OperationsHistory.Domain.Models;
using MAVN.Service.OperationsHistory.Domain.Repositories;
using MAVN.Service.OperationsHistory.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.OperationsHistory.MsSqlRepositories.Repositories
{
    public class TransfersRepository : ITransfersRepository
    {
        private readonly MsSqlContextFactory<OperationsHistoryContext> _contextFactory;

        public TransfersRepository(MsSqlContextFactory<OperationsHistoryContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task AddAsync(ITransfer transfer)
        {
            using (var context = _contextFactory.CreateDataContext())
            {
                var transferEntity = TransferEntity.Create(transfer);
                var historyForSender = TransactionHistoryEntity.CreateForSender(transfer);
                var historyForReceiver = TransactionHistoryEntity.CreateForReceiver(transfer);

                context.Transfers.Add(transferEntity);
                context.TransactionHistories.Add(historyForSender);
                context.TransactionHistories.Add(historyForReceiver);

                await context.SaveChangesAsync();
            }
        }
    }
}
