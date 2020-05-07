using AutoMapper;
using Chess.Users.DataAccess.Entities;
using Chess.Users.DataAccess.Repositories.EntityRepositories;
using Chess.Users.DataAccess.Repositories.Interfaces;
using Chess.Users.Models.UserModels;
using Chess.Users.Models.UserModels.Interfaces;
using Chess.Users.Services.EntityServices.Interfaces;
using Chess.Users.Services.Exceptions;
using Chess.Users.Utilities;
using Chess.Users.Utilities.Interfaces;
using System;
using System.Collections.Generic;
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
            var user = await _repository.GetByEmailAsync(email);

            if (user == default)
                return default;

            return _mapper.Map<UserModel>(user);
        }

        public async Task<bool> DoesUserExistAsync(string email)
            => await _repository.AnyAsync(u => u.Email == email);

        protected override void OnBeforeUpdate(User entity)
        {
            base.OnBeforeUpdate(entity);
        }

        protected override void OnBeforeInsert(User entity)
        {
            base.OnBeforeInsert(entity);

            entity.Password = PasswordHasher.HashPassword(entity.Password);
        }

        public async Task ChangePasswordAsync(Guid userId, IChangePasswordModel model)
        {
            var user = await _repository.GetByIdAsync(userId);

            if (!PasswordHasher.VerifyPassword(model.OldPassword, user.Password))
                throw new ChangePasswordException("Old password is not correct.");

            if (model.NewPassword != model.ConfirmNewPassword)
                throw new ChangePasswordException("Password do not match");

            user.Password = PasswordHasher.HashPassword(model.NewPassword);

            OnBeforeUpdate(user);
            await SaveEntityAsync(user);
        }

        public async Task<IUserDetailsModel> GetUserDetailsAsync(Guid userId)
        {
            if (userId == default)
                throw new ArgumentNullException($"userId");

            var userDetailsDto = await _repository.GetUserDetailsAsync(userId);
            return userDetailsDto;
        }

        public async Task<IUserDetailsModel> UpdateDetailsAsync(Guid userId, IUserDetailsModel model)
        {
            if (model == null)
                throw new ArgumentNullException($"model");

            var user = await _repository.GetByIdAsync(userId);

            _mapper.Map(model, user);
            user = await _repository.SaveAsync(user);
            _mapper.Map(user, model);

            return model;
        }

        public async Task UpdatePointsAsync(Guid userId, IPointsUpdateModel model)
        {
            if (model == null)
                throw new ArgumentNullException($"model");

            var user = await _repository.GetByIdAsync(userId);
            user.Points = model.Points;
            await _repository.SaveAsync(user);
        }

        public async Task<IEnumerable<IUserDetailsModel>> GetAllUserDetailsAsync()
            => await _repository.GetAllUserDetailsAsync();
    }
}