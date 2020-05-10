using Chess.Users.Models.UserModels;
using Chess.Users.Models.Validators.Extensions;
using FluentValidation;

namespace Chess.Users.Models.Validators
{
    public class UserDetailsValidator : AbstractValidator<UserDetailsModel>
    {
        public UserDetailsValidator()
        {
            RuleFor(user => user)
                .NotNull();

            RuleFor(user => user.Email)
                .EmailValidation();

            RuleFor(user => user.Points)
                .PointsValidator();

            RuleFor(user => user.Username)
                .UsernameValidation();
        }
    }
}
