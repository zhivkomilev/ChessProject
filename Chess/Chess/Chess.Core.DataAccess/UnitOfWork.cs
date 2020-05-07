using Chess.Core.DataAccess.Entities;
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
        private readonly Dictionary<Type, object> _repositories;
        private bool disposed;

        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();
        }

        public TRepository GetRepositoryAsync<TRepository, TEntity>()
            where TEntity: class, IBaseEntity
            where TRepository: BaseRepository<TEntity>
        {
            var repoType = typeof(TRepository);
            if (!_repositories.ContainsKey(repoType))
            {
                var repo = (TRepository)Activator.CreateInstance(typeof(TRepository), new object[] { _dbContext.Set<TEntity>() });
                _repositories.Add(repoType, repo);
            }

            return (TRepository)_repositories[repoType];
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