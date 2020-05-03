using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chess.Users.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UsersDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories;
        private bool disposed;

        public UnitOfWork(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Dictionary<Type, object>();
        }

        public TRepository GetRepositoryAsync<TRepository, TEntity>()
            where TEntity : class, IBaseEntity
            where TRepository : BaseRepository<TEntity>
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