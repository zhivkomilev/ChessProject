using Chess.Users.DataAccess.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Chess.Users.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<T>
        where T : IBaseEntity
    {
        Task DeleteAsync(Guid id);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(Guid id);

        Task<T> SaveAsync(T entity);
    }
}