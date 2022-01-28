using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TarotDB2Model
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext?.Set<TEntity>();
        }

        public IQueryable<TEntity> Set => _dbSet;

        public async Task<int> Count() => await _dbSet.CountAsync();

        public virtual async Task<TEntity> FindById(object id)
        {
            var entity = await _dbSet.FindAsync(id);
            //if(NoTracking)
            //{
            //    _dbContext.Entry(entity).State = EntityState.Detached;
            //}
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> GetItems(int index, int count)
        {
            var result = await Task.Run(() => _dbSet.Skip(count * index).Take(count));
            //if (NoTracking)
            //{
            //    return result.AsNoTracking();
            //}
            return result;
        }

        public virtual async Task<TEntity> Insert(TEntity item)
        {
            var result = await _dbSet.AddAsync(item);
            return result.Entity;
        }

        public virtual async Task<bool> AddRange(params TEntity[] items)
        {
            await _dbSet.AddRangeAsync(items);
            return true;
        }

        public virtual async Task<TEntity> Update(TEntity item)
        {
            var result = await Task.Run(() => _dbSet.Update(item));
            return result.Entity;
        }

        public virtual async Task<TEntity> Update(object id, TEntity item)
        {
            var result = await Update(item);
            return result;
        }

        public virtual async Task<bool> Delete(TEntity entity)
        {
            return (await Task.Run(() => _dbSet.Remove(entity)) != null);
        }

        public virtual async Task<bool> Delete(object id)
        {
            var entity = await FindById(id);
            if(entity == null) return false;
            return await Delete(entity);
        }

        public virtual async Task Clear()
        {
            var allEntities = _dbSet.AsEnumerable();
            await Task.Run(() => _dbSet.RemoveRange(allEntities));
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await _dbContext?.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
