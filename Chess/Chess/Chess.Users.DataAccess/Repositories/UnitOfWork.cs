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

        public TRepositoryType GetRepositoryAsync<TRepositoryType, TEntityType>()
            where TRepositoryType : BaseRepository<TEntityType>
            where TEntityType : class, IBaseEntity
        {
            var repoType = typeof(TRepositoryType);

            if (!_repositories.ContainsKey(repoType))
            {
                var repo = (TRepositoryType)Activator.CreateInstance(typeof(TRepositoryType), new object[] { _dbContext.Set<TEntityType>() });
                _repositories.Add(repoType, repo);
            }

            return (TRepositoryType)_repositories[repoType];
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