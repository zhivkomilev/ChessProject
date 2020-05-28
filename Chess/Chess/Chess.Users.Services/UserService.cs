using AutoMapper;
using Chess.Core.DataAccess;
using Chess.Core.Domain;
using Chess.Core.Domain.Interfaces;
using Chess.Core.Services;
using Chess.Users.DataAccess.Entities;
using Chess.Users.Models.UserModels;
using Chess.Users.Services.Exceptions;
using Chess.Users.Services.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Chess.Users.Services
{
    public class UserService : BaseEntityService<User, UserModel>, IUserService
    {
        private readonly ITokenService _tokenService;
        public UserService(IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            IMapper mapper,
            ITokenService tokenService)
            : base(unitOfWork, dateTimeProvider, mapper)
        {
            _tokenService = tokenService;
        }

        public override async Task<IResponse> InsertAsync(UserModel model)
        {
            if (model == null)
                return new Response(HttpStatusCode.BadRequest, false);

            if (await DoesUserExistAsync(model.Email))
                return new Response(HttpStatusCode.BadRequest, false, $"Email already exists.");

            return await base.InsertAsync(model);
        }

        public async Task<IResponse> Login(UserLoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Email))
                return new Response(HttpStatusCode.BadRequest, false, $"Invalid email");

            var user = await _repository.Get(u => u.Email == loginModel.Email);

            if (user == default)
                return new Response(HttpStatusCode.NotFound, false, $"User not found.");

            var model = _mapper.Map<UserModel>(user);
            if (!PasswordHasher.VerifyPassword(loginModel.Password, model.Password))
                return new Response(HttpStatusCode.Unauthorized, false, $"Password incorrect");

            var token = await _tokenService.GenerateJwtAsync(model);

            return new Response(HttpStatusCode.OK, true, new { Token = token });
        }

        public async Task<bool> DoesUserExistAsync(string email)
            => await _repository.AnyAsync(u => u.Email == email);

        protected override void OnBeforeInsert(User entity)
        {
            base.OnBeforeInsert(entity);

            entity.Password = PasswordHasher.HashPassword(entity.Password);
        }

        public async Task<IResponse> ChangePasswordAsync(Guid userId, ChangePasswordModel model)
        {
            var user = await _repository.GetByIdAsync(userId);

            if (!PasswordHasher.VerifyPassword(model.OldPassword, user.Password))
                return new Response(HttpStatusCode.BadRequest, false, "Old password is not correct.");

            user.Password = PasswordHasher.HashPassword(model.NewPassword);

            OnBeforeUpdate(user);
            await SaveEntityAsync(user);

            return new Response(HttpStatusCode.OK, true);
        }

        public async Task<IResponse> GetUserDetailsAsync(Guid userId)
        {
            if (userId == default)
                return new Response(HttpStatusCode.BadRequest, false, $"Invalid user.");

            var userDetailsDto = await _repository.GetDto(u => u.Id == userId, u => new UserDetailsModel
            {
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Points = u.Points,
                Username = u.Username
            });

            return new Response(HttpStatusCode.OK, true, userDetailsDto);
        }

        public async Task<IResponse> UpdateDetailsAsync(Guid userId, UserDetailsModel model)
        {
            var user = await _repository.GetByIdAsync(userId);

            _mapper.Map(model, user);
            user = await _repository.SaveAsync(user);
            _mapper.Map(user, model);

            return new Response(HttpStatusCode.OK, true, model);
        }

        public async Task<IResponse> UpdatePointsAsync(Guid userId, PointsUpdateModel model)
        {
            if (userId == default)
                return new Response(HttpStatusCode.BadRequest, false, $"Invalid user");

            var user = await _repository.GetByIdAsync(userId);
            user.Points = model.Points;
            await _repository.SaveAsync(user);

            return new Response(HttpStatusCode.OK, true);
        }

        public async Task<IResponse> GetAllUserDetailsAsync()
        { 
            var models = await _repository.GetAllDtos(u => new UserDetailsModel
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Points = u.Points,
                    Username = u.Username
                });

            return new Response(HttpStatusCode.OK, true, models);
        } 
    }
}