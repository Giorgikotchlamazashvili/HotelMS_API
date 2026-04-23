using FluentValidation;
using HotelMS.Application.DTOs.Request.Rooms;

namespace HotelMS.Application.Validation.Room
{
    public class CreateRoomValidation : AbstractValidator<CreateRoomRequest>
    {
        public CreateRoomValidation()
        {
            RuleFor(x => x.PricePerNight)
                .GreaterThan(0).WithMessage("Price per night must be greater than 0.");
            RuleFor(x => x.Capacity)
                .InclusiveBetween(1, 10).WithMessage("Capacity must be between 1 and 10.");
            RuleFor(x => x.HotelId)
                .GreaterThan(0).WithMessage("Hotel ID must be greater than 0.");
            RuleFor(x => x.RoomTypeId)
                .GreaterThan(0).WithMessage("Room Type ID must be greater than 0.");
            RuleFor(x => x.AmenityIds)
                .Must(ids => ids.All(id => id > 0)).WithMessage("All Amenity IDs must be greater than 0.");
        }
    }
}
