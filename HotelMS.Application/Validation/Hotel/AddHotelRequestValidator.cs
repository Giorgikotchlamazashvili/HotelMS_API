using FluentValidation;
using HotelMS.Application.DTOs.Request.Hotel;

namespace HotelMS.Application.Validation.Hotel
{
    public class AddHotelRequestValidator : AbstractValidator<AddHotelRequest>
    {
        public AddHotelRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("სასტუმროს სახელი აუცილებელია")
                .Length(3, 100).WithMessage("სახელი უნდა იყოს 3-დან 100 სიმბოლომდე");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("აღწერა აუცილებელია")
                .MaximumLength(500).WithMessage("აღწერა არ უნდა აღემატებოდეს 500 სიმბოლოს")
                .MinimumLength(10).WithMessage("აღწერა უნდა იყოს მინიმუმ 10 სიმბოლოს");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("მისამართი აუცილებელია");

            RuleFor(x => x.Rating)
                .InclusiveBetween(0, 5).WithMessage("რეიტინგი უნდა იყოს 0-დან 5-მდე");

            RuleFor(x => x.CityId)
                .GreaterThan(0).WithMessage("ქალაქის ID უნდა იყოს დადებითი რიცხვი");
        }
    }
}
