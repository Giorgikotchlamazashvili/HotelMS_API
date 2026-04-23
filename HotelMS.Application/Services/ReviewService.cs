using AutoMapper;
using HotelMS.Application.DTOs.Request.ReviewRequest;
using HotelMS.Application.DTOs.Response.ReviewResponse;
using HotelMS.Application.Interfaces.Services;
using HotelMS.Domain.Entities;
using HotelMS.Interfaces.Repositories;


namespace HotelMS.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(
            IReviewRepository reviewRepository,
            IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<(bool success, string error, string data)> CreateReviewAsync(AddReviewRequest request)
        {
            var user = await _reviewRepository.GetUserByEmailAsync(request.Email);
            if (user == null) return (false, "User Not Found", null);

            var hotel = await _reviewRepository.GetHotelByIdAsync(request.HotelId);
            if (hotel == null) return (false, "Hotel not found.", null);

            var review = new Reviews
            {
                Rating = request.Rating,
                Comment = request.Comment,
                UserId = user.Id,
                HotelId = request.HotelId
            };

            await _reviewRepository.AddReviewAsync(review);
            await _reviewRepository.SaveChangesAsync();

            await UpdateHotelRatingInternal(request.HotelId);

            return (true, null, "Review created successfully.");
        }

        public async Task<(bool success, string error, string data)> DeleteReviewAsync(DeleteReviewRequest req, int userId)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(req.Id);

            if (review == null) return (false, "Review not found.", null);

            if (review.UserId != userId)
                return (false, "You can only delete your own review.", null);

            int hotelId = review.HotelId;

            await _reviewRepository.RemoveReviewAsync(review);
            await _reviewRepository.SaveChangesAsync();


            await UpdateHotelRatingInternal(hotelId);

            return (true, null, "Review deleted successfully.");
        }

        public async Task<(bool success, string error, double averageRating)> CalculateHotelRatingAsync(int hotelId)
        {
            var reviews = await _reviewRepository.GetReviewsByHotelIdAsync(hotelId);

            if (reviews == null || !reviews.Any())
            {
                return (true, null, 0);
            }

            double average = reviews.Average(r => r.Rating);
            return (true, null, Math.Round(average, 1));
        }


        private async Task UpdateHotelRatingInternal(int hotelId)
        {
            var hotel = await _reviewRepository.GetHotelByIdAsync(hotelId);

            if (hotel != null)
            {
                var ratingResult = await CalculateHotelRatingAsync(hotelId);

                hotel.Rating = ratingResult.averageRating;
                await _reviewRepository.SaveChangesAsync();
            }
        }

        public async Task<(bool success, string error, List<GetReviewResponse> data)> GetReviewsByUserAsync(int userId)
        {
            var reviews = await _reviewRepository.GetReviewsByUserIdAsync(userId);
            var result = _mapper.Map<List<GetReviewResponse>>(reviews);
            return (true, null, result);
        }
    }
}
