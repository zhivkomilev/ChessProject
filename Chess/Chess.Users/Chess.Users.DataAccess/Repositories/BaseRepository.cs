using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Chess.Users.DataAccess.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(DbSet<TEntity> dbset)
            => _dbSet = dbset;

        public async Task<TEntity> GetByIdAsync(Guid id)
            => await _dbSet.FindAsync(id);

        public async Task SaveAsync(TEntity entity)
        {
            if (entity.Id != default)
            {
                await UpdateAsync(entity);
            }
            else
            {
                await InsertAsync(entity);
            }
        }

        private async Task UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        private async Task InsertAsync(TEntity entity)
            => await _dbSet.AddAsync(entity);
    }
}