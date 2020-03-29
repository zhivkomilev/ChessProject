﻿using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories;
using Chess.Users.Models.EntityModels;
using System;
using System.Threading.Tasks;

namespace Chess.Users.Services.EntityServices.Interfaces
{
    public interface IBaseEntityService<TEntity, TModel, TRepositoryType>
        where TEntity : class, IBaseEntity
        where TModel : BaseModel
        where TRepositoryType : BaseRepository<TEntity>
    {
        Task<TModel> GetByIdAsync(Guid id);
        Task InsertAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task SaveChangesAsync();
    }
}
