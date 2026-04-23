using FluentValidation;
using HotelMS.Application.DTOs.Request.ReviewRequest;


namespace HotelMS.Application.Validation.Review
{
    public class AddReviewrRequestValidation : AbstractValidator<AddReviewRequest>
    {
        public AddReviewrRequestValidation()
        {
            RuleFor(x => x.HotelId)
                .GreaterThan(0).WithMessage("Hotel ID must be a positive integer.");
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
            RuleFor(x => x.Comment)
                .MaximumLength(500).WithMessage("Comment cannot exceed 500 characters.");
        }
    }
}
