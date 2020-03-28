using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories;
using Chess.Users.Models.EntityModels;
using System.Threading.Tasks;

namespace Chess.Users.Services.EntityServices.Interfaces
{
    public interface IBaseEntityService<TEntity, TModel, TRepositoryType>
        where TEntity : class, IBaseEntity
        where TModel : class, IBaseModel
        where TRepositoryType : BaseRepository<TEntity>
    {
        Task Insert(TModel model);
        Task Update(TModel model);
        Task SaveChangesAsync();
    }
}
