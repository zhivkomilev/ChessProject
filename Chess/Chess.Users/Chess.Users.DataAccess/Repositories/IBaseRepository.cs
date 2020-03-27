using Chess.Users.DataAccess.Entities;
using System.Threading.Tasks;

namespace Chess.Users.DataAccess.Repositories
{
    public interface IBaseRepository<T> : IRepository
        where T : IBaseEntity
    {
        Task<T> GetByIdAsync(int id);

        Task SaveAsync(T entity);
    }
}