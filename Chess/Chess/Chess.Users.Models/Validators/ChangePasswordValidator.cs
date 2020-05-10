using Chess.Users.Models.UserModels;
using Chess.Users.Models.Validators.Extensions;
using FluentValidation;

namespace Chess.Users.Models.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(model => model.NewPassword)
                .NewPasswordValidation();
        }
    }
}