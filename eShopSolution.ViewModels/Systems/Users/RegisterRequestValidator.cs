using eShopSolution.Utilities.Constants;
using FluentValidation;
using System;

namespace eShopSolution.ViewModels.Systems.Users
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name is require");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is require")
                                    .MinimumLength(ModelValidationConstants.PASSWORD_MIN_LENGTH)
                                        .WithMessage($"Password is at least {ModelValidationConstants.PASSWORD_MIN_LENGTH} characters")
                                    .MaximumLength(ModelValidationConstants.PASSWORD_MAX_LENGTH)
                                        .WithMessage($"Password cannot over {ModelValidationConstants.PASSWORD_MAX_LENGTH} characters");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Confirm password must be equal to password");

            RuleFor(x => x.Dob).GreaterThan(DateTime.Now.AddYears(-ModelValidationConstants.MAX_DATE_OF_BIRTH))
                                    .WithMessage("Date of birth must be after 01/01/1920");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is require")
                                 .EmailAddress().WithMessage("A valid email is required");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is require")
                                       .Length(ModelValidationConstants.PHONE_NUMBER_MAX_LENGTH)
                                            .WithMessage($"Phone number must be equal {ModelValidationConstants.PHONE_NUMBER_MAX_LENGTH} characters");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is require")
                                     .MaximumLength(ModelValidationConstants.FIRST_NAME_MAX_LENGTH)
                                        .WithMessage($"First name cannot over {ModelValidationConstants.FIRST_NAME_MAX_LENGTH} characters");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is require")
                                     .MaximumLength(ModelValidationConstants.LAST_NAME_MAX_LENGTH)
                                        .WithMessage($"Last name cannot over {ModelValidationConstants.LAST_NAME_MAX_LENGTH} characters");
        }
    }
}
