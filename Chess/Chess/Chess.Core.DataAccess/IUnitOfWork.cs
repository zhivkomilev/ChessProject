using Chess.Core.DataAccess.Entities;
using System;
using System.Threading.Tasks;


namespace Chess.Core.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IBaseEntity;

        Task RollbackAsync();

        Task<int> SaveChangesAsync();
    }
}
