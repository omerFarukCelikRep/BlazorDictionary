using BlazorDictionary.Common.Models.RequestModels;
using FluentValidation;

namespace BlazorDictionary.Api.Application.Features.Commands.Users;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage("{PropertyName} cannot be null")
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible).WithMessage("{PropertyName} not a valid email address");

        RuleFor(x => x.Password)
             .NotNull().WithMessage("{PropertyName} cannot be null")
             .NotEmpty().WithMessage("{PropertyName} cannot be empty")
             .MinimumLength(6).WithMessage("{PropertyName} should at least be {MinLength} characters");
    }
}
