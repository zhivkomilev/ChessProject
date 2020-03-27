using Chess.Users.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Chess.Users.DataAccess.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DbSet<TEntity> dbset)
        {
            _dbSet = dbset;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveAsync(TEntity entity)
        {
            if (entity.Id > 0)
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
            entity.LatestUpdateDate = DateTime.Now;

            _dbSet.Update(entity);

            await Task.CompletedTask;
        }

        private async Task InsertAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.LatestUpdateDate = DateTime.Now;

            await _dbSet.AddAsync(entity);
        }

    }
}
