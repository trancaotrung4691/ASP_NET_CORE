using eShopSolution.Utilities.Constants;
using FluentValidation;

namespace eShopSolution.ViewModels.Systems.Users
{
    public class LoginRequestValidator :AbstractValidator<LoginRequest>
    {
        
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is require");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is require")
                                    .MinimumLength(ModelValidationConstants.PASSWORD_MIN_LENGTH).WithMessage($"Password is atleast {ModelValidationConstants.PASSWORD_MIN_LENGTH} characters")
                                    .MaximumLength(ModelValidationConstants.PASSWORD_MAX_LENGTH).WithMessage($"Password cannot over {ModelValidationConstants.PASSWORD_MAX_LENGTH} characters");
        }
    }
}
