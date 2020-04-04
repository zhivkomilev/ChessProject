using AutoMapper;
using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.EntityRepositories;
using Chess.Users.DataAccess.Repositories.Interfaces;
using Chess.Users.Models.EntityModels.UserModels;
using Chess.Users.Models.EntityModels.UserModels.Interfaces;
using Chess.Users.Services.EntityServices.Interfaces;
using Chess.Users.Utilities;
using Chess.Users.Utilities.Interfaces;
using System.Threading.Tasks;

namespace Chess.Users.Services.EntityServices
{
    public class UserService : BaseEntityService<User, UserModel, UserRepository>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork, 
            IDateTimeProvider dateTimeProvider, 
            IMapper mapper) 
            : base(unitOfWork, dateTimeProvider, mapper) { }

        public async Task<IUserModel> GetByUsernameAsync(string username, string password)
        {
            var repo = await _unitOfWork.GetRepositoryAsync<UserRepository, User>();
            var user = await repo.GetByUsernameAsync(username);

            if (user == default)
                return default;
            
            return _mapper.Map<UserModel>(user);
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
    }
}