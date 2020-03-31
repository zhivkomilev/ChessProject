using Chess.Users.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace Chess.Users.DataAccess.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<TRepositoryType> GetRepositoryAsync<TRepositoryType, TEntityType>()
            where TEntityType : class, IBaseEntity
            where TRepositoryType : BaseRepository<TEntityType>;

        Task RollbackAsync();

        Task<int> SaveChangesAsync();
    }
}
