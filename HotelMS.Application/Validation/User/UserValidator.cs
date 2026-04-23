using FluentValidation;
using HotelMS.Application.DTOs.UserRequest;

namespace HotelMS.Application.Validation.User;

public class UserValidator : AbstractValidator<CreateUser>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long.");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Emaili Is required")
            .EmailAddress().WithMessage("Invalid email format.");
        RuleFor(u => u.Password)
            .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")
            .WithMessage("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, and one number.");
    }
}
