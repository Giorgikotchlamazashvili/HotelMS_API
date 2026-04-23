using FluentValidation;
using HotelMS.Application.DTOs.Request.UserDetailsRequest;

namespace HotelMS.Application.Validation.UserDetals
{
    public class UserDetailsValidation : AbstractValidator<UpdateUserDetails>
    {
        public UserDetailsValidation()
        {

            RuleFor(x => x.Mail)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.FirstName)
                .MinimumLength(2).WithMessage("First name must be at least 2 characters.")
                .When(x => !string.IsNullOrEmpty(x.FirstName));
            RuleFor(x => x.Age)
                .InclusiveBetween(18, 100).WithMessage("Age must be between 18 and 100.")
                .When(x => x.Age.HasValue);
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\d{9,15}$").WithMessage("Phone number must be between 9 and 15 digits.")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
    }
}