using FluentValidation;
using HotelMS.Application.DTOs.Request.Rooms;

namespace HotelMS.Application.Validation.Room
{
    public class CreateAmeniyValidation : AbstractValidator<CreateAmenityRequest>

    {
        public CreateAmeniyValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Amenity name is required.")
                .Length(2, 100).WithMessage("Amenity name must be between 2 and 100 characters.");
        }
    }
}
