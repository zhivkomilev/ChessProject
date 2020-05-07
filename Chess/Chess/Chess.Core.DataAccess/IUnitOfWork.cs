using Chess.Core.DataAccess.Entities;
using System;
using System.Threading.Tasks;


namespace Chess.Core.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        TRepositoryType GetRepositoryAsync<TRepositoryType, TEntityType>()
            where TEntityType : class, IBaseEntity
            where TRepositoryType : BaseRepository<TEntityType>;

        Task RollbackAsync();

        Task<int> SaveChangesAsync();
    }
}
