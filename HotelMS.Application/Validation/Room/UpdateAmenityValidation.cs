using FluentValidation;
using HotelMS.Application.DTOs.Request.Rooms;

namespace HotelMS.Application.Validation.Room
{
    public class UpdateAmenityValidation : AbstractValidator<UpdateAmenityRequest>
    {
        public UpdateAmenityValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Amenity name is required.")
                .Length(2, 100).WithMessage("Amenity name must be between 2 and 100 characters.");
        }
    }
}
