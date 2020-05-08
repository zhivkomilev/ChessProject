using Chess.Core.DataAccess.Entities;
using Chess.Core.DataAccess.Wrappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chess.Core.DataAccess
{
    public class UnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;
        private readonly IActivatorWrapper _activator;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private bool disposed;

        public UnitOfWork(TDbContext dbContext,
            IActivatorWrapper activator)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _activator = activator ?? throw new ArgumentNullException(nameof(activator));
        }
        
        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IBaseEntity
        {
            var entityType = typeof(TEntity);

            if (!_repositories.ContainsKey(entityType))
            {
                var repo = (IRepository<TEntity>)_activator.CreateInstance<Repository<TEntity>>(new object[] { _dbContext.Set<TEntity>() });
                _repositories.Add(entityType, repo);
            }

            return (IRepository<TEntity>)_repositories[entityType];
        }

        public async Task<int> SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();
        
        public async Task RollbackAsync()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());

            await Task.CompletedTask;
        }

        #region Dispose pattern
        public void Dispose()
            => Dispose(true);

        private void Dispose(bool disposing)
        {
            if (disposed || !disposing) return;

            _dbContext.Dispose();
            disposed = true;
        }
        #endregion
    }
}