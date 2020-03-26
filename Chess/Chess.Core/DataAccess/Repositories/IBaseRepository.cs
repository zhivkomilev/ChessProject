using Chess.Core.DataAccess.Repositories;
using Chess.Core.Entities;

namespace Chess.Core.Repositories
{
    public interface IBaseRepository<T> : IRepository
        where T : IBaseEntity
    {
        T GetById(int id);

        T Save(T entity);
    }
}