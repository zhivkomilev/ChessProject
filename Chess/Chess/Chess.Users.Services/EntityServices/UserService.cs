using AutoMapper;
using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.EntityRepositories;
using Chess.Users.DataAccess.Repositories.Interfaces;
using Chess.Users.Models.EntityModels.UserModels;
using Chess.Users.Models.EntityModels.UserModels.Interfaces;
using Chess.Users.Services.EntityServices.Interfaces;
using Chess.Users.Utilities;
using Chess.Users.Utilities.Interfaces;
using System;
using System.Threading.Tasks;

namespace Chess.Users.Services.EntityServices
{
    public class UserService : BaseEntityService<User, UserModel, UserRepository>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, 
            IDateTimeProvider dateTimeProvider, 
            IMapper mapper) 
            : base(unitOfWork, dateTimeProvider, mapper) { }

        public async Task<IUserModel> GetByEmailAsync(string email)
        {
            var repo = await _unitOfWork.GetRepositoryAsync<UserRepository, User>();
            var user = await repo.GetByEmailAsync(email);

            if (user == default)
                return default;
            
            return _mapper.Map<UserModel>(user);
        }

        public async Task<bool> DoesUserExistAsync(string email)
        {
            var repo = await _unitOfWork.GetRepositoryAsync<UserRepository, User>();

            return await repo.AnyAsync(u => u.Email == email);
        }
        protected override void OnBeforeUpdate(User entity)
        {
            base.OnBeforeUpdate(entity);
        }

        protected override void OnBeforeInsert(User entity)
        {
            base.OnBeforeInsert(entity);

            entity.Password = PasswordHasher.HashPassword(entity.Password);
        }

        public Task RequestPasswordChange(Guid userId)
        {
            throw new System.NotImplementedException();
        }
    }
}