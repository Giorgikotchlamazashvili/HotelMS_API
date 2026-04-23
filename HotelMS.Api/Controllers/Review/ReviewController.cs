using System.Security.Claims;
using HotelMS.Application.DTOs.Request.ReviewRequest;
using HotelMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelMS.Api.Controllers.Review
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateReview(AddReviewRequest request)
        {
            var (success, error, data) = await _reviewService.CreateReviewAsync(request);

            if (!success)
            {
                return NotFound(error);
            }

            return Ok(data);
        }

        [Authorize]
        [HttpGet("user-reviews")]
        public async Task<IActionResult> GetReviewsByUser()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var (success, error, data) = await _reviewService.GetReviewsByUserAsync(userId);

            if (!success)
            {
                return NotFound(error);
            }

            return Ok(data);
        }


        [HttpDelete("delete-user-review")]
        public async Task<IActionResult> DeleteReview(DeleteReviewRequest req)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var (success, error, data) = await _reviewService.DeleteReviewAsync(req, userId);

            if (!success)
            {
                return BadRequest(error);
            }

            return Ok(data);
        }
    }
}