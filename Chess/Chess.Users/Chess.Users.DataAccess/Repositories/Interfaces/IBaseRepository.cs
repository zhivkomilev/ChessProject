using Chess.Users.DataAccess.Entities;
using System;
using System.Threading.Tasks;

namespace Chess.Users.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T>
        where T : IBaseEntity
    {
        Task<T> GetByIdAsync(Guid id);

        Task SaveAsync(T entity);
    }
}