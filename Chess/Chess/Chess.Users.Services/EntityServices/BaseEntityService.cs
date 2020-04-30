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
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IDateTimeProvider _dateTimeProvider;
        protected readonly IMapper _mapper;
        protected readonly TRepositoryType _repository;

        protected BaseEntityService(IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
            _repository = _unitOfWork.GetRepositoryAsync<TRepositoryType, TEntity>();
        }
        
        public async Task<TModel> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            return _mapper.Map<TModel>(entity); ;
        }

        public async Task<TModel> InsertAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            OnBeforeInsert(entity);
            model = await SaveEntityAsync(entity);
            
            return model;
        }

        public async Task<TModel> UpdateAsync(TModel model)
        {
            var entity = _mapper.Map<TEntity>(model);
            OnBeforeUpdate(entity);
            model = await SaveEntityAsync(entity);

            return model;
        }

        public async Task DeleteAsync(Guid id)
            =>  await _repository.DeleteAsync(id);
        
        public async Task SaveChangesAsync()
            => await _unitOfWork.SaveChangesAsync();

        protected virtual async Task<TModel> SaveEntityAsync(TEntity entity)
        {
            var model = _mapper.Map<TModel>(await _repository.SaveAsync(entity));

            return model;
        }

        protected virtual void OnBeforeUpdate(TEntity entity)
            => entity.LatestUpdateDate= _dateTimeProvider.UtcNow;
        
        protected virtual void OnBeforeInsert(TEntity entity)
           => entity.LatestUpdateDate = entity.CreatedDate = _dateTimeProvider.UtcNow;

        #region IDisposable Support
        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || !disposing)
                return;

            _unitOfWork.Dispose();
            _disposed = true;
        }

        public void Dispose()
            => Dispose(true);
        #endregion
    }
}