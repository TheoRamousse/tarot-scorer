using System;
using System.Threading;
using System.Threading.Tasks;

namespace TarotDB2Model
{
    interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task RejectChangesAsync();
    }

}
