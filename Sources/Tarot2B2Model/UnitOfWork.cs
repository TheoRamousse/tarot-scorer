using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TarotDB2Model
{
    public  class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public UnitOfWork(DbContext context, bool noTracking = true)
        {
            _dbContext = context;
            NoTracking = noTracking;
            if (NoTracking)
            {
                _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            }
        }

        bool NoTracking { get; set; }

        public void Dispose()
        {
            _dbContext?.Dispose();
        }

        public virtual async Task RejectChangesAsync()
        {
            await Task.Run(() =>
            {
                foreach (var entry in _dbContext.ChangeTracker.Entries()
                    .Where(e => e.State != EntityState.Unchanged))
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                        case EntityState.Modified:
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                    }
                }
            });
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = _dbContext.ChangeTracker.Entries();
            //foreach (var entity in _dbContext.ChangeTracker.Entries()
            //    .Where(e => e.State == EntityState.Added))
            //{
            //    if(entity.IsKeySet)
            //    {
            //        entity.State = EntityState.Unchanged;
            //    }
            //}

            var result = await _dbContext?.SaveChangesAsync(cancellationToken);
            foreach (var entity in _dbContext.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Detached))
            {
                entity.State = EntityState.Detached;
            }
            return result;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_dbContext);
        }
    }

}
