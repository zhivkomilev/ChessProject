using Chess.Users.Models.UserModels;
using Chess.Users.Models.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Chess.Users.Models.ServiceExtensions
{
    public static class ServicesExtensions
    {
        public static void AddUserServiceValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserModel>, UserValidator>();
            services.AddScoped<IValidator<UserDetailsModel>, UserDetailsValidator>();
            services.AddScoped<IValidator<PointsUpdateModel>, UserPointsValidator>();
            services.AddScoped<IValidator<ChangePasswordModel>, ChangePasswordValidator>();
            services.AddScoped<IValidator<UserRegisterModel>, UserRegisterValidator>();
        }
    }
}