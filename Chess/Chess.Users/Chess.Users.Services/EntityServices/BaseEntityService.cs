using AutoMapper;
using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories;
using Chess.Users.DataAccess.Repositories.Interfaces;
using Chess.Users.Models.EntityModels;
using Chess.Users.Services.EntityServices.Interfaces;
using Chess.Users.Utilities.Interfaces;
using System;
using System.Threading.Tasks;

namespace Chess.Users.Services.EntityServices
{
    public abstract class BaseEntityService<TEntity, TModel, TRepositoryType> : IBaseEntityService<TEntity, TModel, TRepositoryType>
        where TEntity: class, IBaseEntity
        where TModel: class, IBaseModel
        where TRepositoryType: BaseRepository<TEntity>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        protected readonly IMapper _mapper;

        protected BaseEntityService(IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
        }
        
        public async Task<TModel> GetByIdAsync(Guid id)
        {
            var repo = await _unitOfWork.GetRepositoryAsync<TRepositoryType, TEntity>();
            var entity = await repo.GetByIdAsync(id);

            return _mapper.Map<TModel>(entity);
        }

        public async Task InsertAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            OnBeforeInsert(entity);

            var repo = await _unitOfWork.GetRepositoryAsync<TRepositoryType, TEntity>();

            await repo.SaveAsync(entity);
        }

        public async Task UpdateAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            OnBeforeUpdate(entity);

            var repo = await _unitOfWork.GetRepositoryAsync<TRepositoryType, TEntity>();

            await repo.SaveAsync(entity);
        }

        public async Task SaveChangesAsync()
            => await _unitOfWork.CommitAsync();
        
        public async Task DeleteAsync(Guid id)
        {
            var repo = await _unitOfWork.GetRepositoryAsync<TRepositoryType, TEntity>();
            
            await repo.DeleteAsync(id);
        }

        protected virtual void OnBeforeUpdate(TEntity entity)
            => entity.LatestUpdateDate= _dateTimeProvider.UtcNow;
        
        protected virtual void OnBeforeInsert(TEntity entity)
           => entity.LatestUpdateDate = entity.CreatedDate = _dateTimeProvider.UtcNow;
    }
}