using FluentValidation;
using HotelMS.Application.DTOs.Request.Booking;

namespace HotelMS.Application.Validation.Booking
{
    public class BookingValidation : AbstractValidator<CreateBookRequest>
    {
        public BookingValidation()
        {
            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("ოთახის არჩევა აუცილებელია.")
                .GreaterThan(0).WithMessage("ოთახის ID უნდა იყოს დადებითი რიცხვი.");

            RuleFor(x => x.CheckInDate)
                .NotEmpty().WithMessage("მისვლის თარიღი აუცილებელია.")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("მისვლის თარიღი არ შეიძლება იყოს წარსულში.");

            RuleFor(x => x.CheckOutDate)
                .NotEmpty().WithMessage("გამგზავრების თარიღი აუცილებელია.")
                .GreaterThan(x => x.CheckInDate).WithMessage("გამგზავრების თარიღი უნდა იყოს მისვლის თარიღზე გვიან.");

            RuleFor(x => x.CheckOutDate)
                .Must((request, checkout) => (checkout - request.CheckInDate).TotalDays >= 1)
                .WithMessage("დაჯავშნა მინიმუმ 1 დღით არის შესაძლებელი.");
        }
    }
}