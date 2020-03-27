using Chess.Users.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace Chess.Users.DataAccess.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<TRepositoryType> GetRepositoryAsync<TRepositoryType, TEntityType>()
            where TRepositoryType : IRepository
            where TEntityType : class, IBaseEntity;

        Task RollbackAsync();

        Task<int> CommitAsync();
    }
}
