

using HotelMS.Application.DTOs.Request.ReviewRequest;
using HotelMS.Application.DTOs.Response.ReviewResponse;

namespace HotelMS.Application.Interfaces.Services;
public interface IReviewService
{
    Task<(bool success, string error, string data)> CreateReviewAsync(AddReviewRequest request);
    Task<(bool success, string error, List<GetReviewResponse> data)> GetReviewsByUserAsync(int userId);
    Task<(bool success, string error, string data)> DeleteReviewAsync(DeleteReviewRequest req, int userId);
}