using System;
using System.Threading;
using System.Threading.Tasks;
using TarotDB;

namespace TarotDB2Model
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task RejectChangesAsync();
    }

}
