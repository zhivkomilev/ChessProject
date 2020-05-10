using Chess.Users.Models.Resources;
using Chess.Users.Models.UserModels;
using FluentValidation;
using FluentValidation.Validators;

namespace Chess.Users.Models.Validators.Extensions
{
    public static class UserValidatorExtensions
    {
        public static IRuleBuilderOptions<T, int> PointsValidator<T>(this IRuleBuilder<T, int> rule, int greaterThanOrEqualTo = 0)
        {
            return rule.GreaterThanOrEqualTo(greaterThanOrEqualTo);
        }

        public static IRuleBuilderOptions<T, string> EmailValidation<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotNull()
                .WithMessage(ValidationErrorMessages.EmptyEmail)
                .NotEmpty()
                .WithMessage(ValidationErrorMessages.EmptyEmail)
                .EmailAddress(EmailValidationMode.Net4xRegex)
                .WithMessage(ValidationErrorMessages.InvalidEmail);
        }

        public static IRuleBuilderOptions<T, string> UsernameValidation<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotNull()
                .WithMessage(ValidationErrorMessages.UsernameEmpty)
                .NotEmpty()
                .WithMessage(ValidationErrorMessages.UsernameEmpty);
        }

        public static IRuleBuilderOptions<T, string> PasswordValidation<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotNull()
                .WithMessage(ValidationErrorMessages.EmptyPassword)
                .NotEmpty()
                .WithMessage(ValidationErrorMessages.EmptyPassword)
                .MinimumLength(8)
                .WithMessage(ValidationErrorMessages.PasswordMinimumLength)
                .Matches($"(?=.*[0-9])")
                .WithMessage(ValidationErrorMessages.PasswordAtLeastOneNumber);
        }

        public static IRuleBuilderOptions<T, string> NewPasswordValidation<T>(this IRuleBuilder<T, string> rule)
            where T : ChangePasswordModel
        {
            return rule
                .PasswordValidation()
                .Equal(model => model.ConfirmNewPassword)
                .WithMessage(ValidationErrorMessages.ConfirmNewPassword);
        }
    }
}