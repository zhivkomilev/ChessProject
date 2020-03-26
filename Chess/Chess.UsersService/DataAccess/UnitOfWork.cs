using Chess.Core.DataAccess.Repositories;
using Chess.Core.DataAccess.UnitOfWork;
using Chess.UsersService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chess.UsersService.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UsersDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        private bool disposed;

        public UnitOfWork(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TRepositoryInterface> RepositoryAsync<TRepositoryInterface>()
            where TRepositoryInterface : IRepository
        {
            var repoType = typeof(TRepositoryInterface);

            if (!_repositories.ContainsKey(repoType))
            {
                var repo = (TRepositoryInterface)Activator.CreateInstance(typeof(TRepositoryInterface), new object[] { _dbContext });
                _repositories.Add(repoType, repo);
            }

            return await Task.FromResult((TRepositoryInterface)_repositories[repoType]);
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposed || !disposing) return;

            _dbContext.Dispose();
            disposed = true;
        }
        public async Task RollbackAsync()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());

            await Task.CompletedTask;
        }
    }
}