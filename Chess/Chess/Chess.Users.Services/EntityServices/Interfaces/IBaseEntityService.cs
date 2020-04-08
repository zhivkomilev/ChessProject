using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories;
using Chess.Users.Models.EntityModels;
using System;
using System.Threading.Tasks;

namespace Chess.Users.Services.EntityServices.Interfaces
{
    public interface IBaseEntityService<TEntity, TModel, TRepositoryType>
        where TEntity : class, IBaseEntity
        where TModel : class, IBaseModel
        where TRepositoryType : BaseRepository<TEntity>
    {
        /// <summary>
        /// Deletes TEntity with given id.
        /// </summary>
        /// <param name="id">Id of an existing user.</param>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Retrieves TModel with the given id
        /// </summary>
        /// <param name="id">Id of an existing user</param>
        /// <returns>Return TModel with the pass id</returns>
        Task<TModel> GetByIdAsync(Guid id);

        /// <summary>
        /// Inserts new model to DbSet
        /// </summary>
        /// <param name="model">Model to be inserted</param>
        /// <returns>The inserted model</returns>
        Task<TModel> InsertAsync(TModel model);

        /// <summary>
        /// Updates the model in the DbSet
        /// </summary>
        /// <param name="model">Model to be updated</param>
        /// <returns>Updated model</returns>
        Task<TModel> UpdateAsync(TModel model);

        /// <summary>
        /// Saves all changes made to the DbContext or DbSet
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
