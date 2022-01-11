using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TarotDB2Model
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> Insert(TEntity item);

        Task<TEntity> Update(object id, TEntity item);

        Task<bool> AddRange(params TEntity[] items);

        Task<TEntity> FindById(object id);

        Task<IEnumerable<TEntity>> GetItems(int index, int count);

        Task<bool> Delete(object id);

        Task<bool> Delete(TEntity entity);

        Task Clear();

        Task<int> Count();

        IQueryable<TEntity> Set { get; }

        Task<TEntity> Update(TEntity item);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
