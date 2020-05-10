using Chess.Users.Models.UserModels;
using Chess.Users.Models.Validators.Extensions;
using FluentValidation;

namespace Chess.Users.Models.Validators
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterModel>
    {
        public UserRegisterValidator()
        {
            RuleFor(model => model.Email)
                .EmailValidation();

            RuleFor(model => model.Password)
                .PasswordValidation();

            RuleFor(model => model.Username)
                .UsernameValidation();
        }
    }
}
