using Chess.Users.Models.UserModels;
using Chess.Users.Models.Validators.Extensions;
using FluentValidation;

namespace Chess.Users.Models.Validators
{
    public class UserPointsValidator : AbstractValidator<PointsUpdateModel>
    {
        public UserPointsValidator()
        {
            RuleFor(model => model)
                .NotNull();

            RuleFor(model => model.Points)
                .PointsValidator();
        }
    }
}