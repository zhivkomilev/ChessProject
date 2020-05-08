using Chess.Core.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Chess.Core.DataAccess
{
    public interface IRepository<T>
        where T : IBaseEntity
    {
        Task DeleteAsync(Guid id);

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        Task<T> Get(Expression<Func<T, bool>> filter);

        Task<TDto> GetDto<TDto>(Expression<Func<T, bool>> filter, Expression<Func<T, TDto>> select);

        Task<IEnumerable<TDto>> GetAllDtos<TDto>(Expression<Func<T, bool>> filter, Expression<Func<T, TDto>> select);

        Task<IEnumerable<TDto>> GetAllDtos<TDto>(Expression<Func<T, TDto>> select);

        Task<T> GetByIdAsync(Guid id);

        Task<T> SaveAsync(T entity);
    }
}