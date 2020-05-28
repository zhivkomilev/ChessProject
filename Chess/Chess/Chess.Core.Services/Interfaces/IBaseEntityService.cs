using Chess.Core.DataAccess.Entities;
using Chess.Core.Domain.Interfaces;
using Chess.Core.Models;
using System;
using System.Threading.Tasks;

namespace Chess.Core.Services.Interfaces
{
    public interface IBaseEntityService<TEntity, TModel> : IDisposable
        where TEntity : class, IBaseEntity
        where TModel : IBaseModel
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
        Task<IResponse> GetByIdAsync(Guid id);

        /// <summary>
        /// Inserts new model to DbSet
        /// </summary>
        /// <param name="model">Model to be inserted</param>
        /// <returns>The inserted model</returns>
        Task<IResponse> InsertAsync(TModel model);

        /// <summary>
        /// Updates the model in the DbSet
        /// </summary>
        /// <param name="model">Model to be updated</param>
        /// <returns>Updated model</returns>
        Task<IResponse> UpdateAsync(TModel model);

        /// <summary>
        /// Saves all changes made to the DbContext or DbSet
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
