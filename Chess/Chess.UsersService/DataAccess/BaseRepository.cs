using Chess.Core.Entities;
using Chess.Core.Repositories;
using Chess.UsersService.Data;
using System;

namespace Chess.UsersService.DataAccess
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : IBaseEntity
    {
        private readonly UsersDbContext _dbContext;
        public BaseRepository(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TEntity Save(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
