using FluentValidation;
using HotelMS.Application.DTOs.Request.Rooms;

namespace HotelMS.Application.Validation.Room
{
    public class UpdateRoomTypeValidation : AbstractValidator<UpdateRoomTypeRequest>
    {
        public UpdateRoomTypeValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Room type name is required.")
                .Length(3, 50).WithMessage("Room type name must be between 3 and 50 characters.");
            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");
        }
    }
}
