using Chess.Core.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Chess.Core.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbSet<TEntity> dbSet)
            => _dbSet = dbSet;

        public async Task<TEntity> GetByIdAsync(Guid id)
            => await _dbSet.FindAsync(id);

        public async Task<TDto> GetDto<TDto>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity,TDto>> select)
        {
            var dto = await _dbSet.Where(filter).Select(select).SingleOrDefaultAsync();
            
            return dto;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
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

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(filter);

            return entity;
        }

        public async Task<IEnumerable<TDto>> GetAllDtos<TDto>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TDto>> select)
        {
            var dtos = await _dbSet.Where(filter).Select(select).ToListAsync();

            return dtos;
        }

        public async Task<IEnumerable<TDto>> GetAllDtos<TDto>(Expression<Func<TEntity, TDto>> select)
        {
            var dtos = await _dbSet.Select(select).ToListAsync();

            return dtos;
        }
    }
}