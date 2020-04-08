using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
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

        public async Task<bool> AnyAsync(Expression<Func<TEntity,bool>> predicate) 
            => await _dbSet.AnyAsync(predicate);

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            _dbSet.Remove(entity);
        }

        public async Task<TEntity> SaveAsync(TEntity entity)
        {
            if (entity.Id != default)
            {
                return await UpdateAsync(entity);
            }
            else
            {
                return await InsertAsync(entity);
            }
        }

        private async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var updatedEntity = _dbSet.Update(entity).Entity;
            return await Task.FromResult(updatedEntity);
        }

        private async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entityEntry = await _dbSet.AddAsync(entity);
            return await Task.FromResult(entityEntry.Entity);
        }
    }
}