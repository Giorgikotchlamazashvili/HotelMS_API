using FluentValidation;
using HotelMS.Application.DTOs.Request.Rooms;

namespace HotelMS.Application.Validation.Room
{
    public class UpdateRoomValidation : AbstractValidator<UpdateRoomRequest>
    {
        public UpdateRoomValidation()
        {
            RuleFor(x => x.PricePerNight)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Capacity must be greater than zero.");
        }
    }
}
