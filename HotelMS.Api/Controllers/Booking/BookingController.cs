using System.Security.Claims;
using HotelMS.Application.DTOs.Request.Booking;
using HotelMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelMS.Api.Controllers.Booking
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [Authorize(Roles = "Manager, Receptionist")]
        [HttpGet("get-all-booking")]
        public async Task<IActionResult> GetAllBooking()
        {
            var bookings = await _bookingService.GetAllBookingsAsync();
            return Ok(bookings);
        }


        [Authorize(Roles = "Manager, Receptionist")]
        [HttpGet("get-booking-by-id")]
        public async Task<IActionResult> GetBookingByID(int id)
        {
            var result = await _bookingService.GetBookingByIdAsync(id);

            if (!result.success)
                return NotFound(result.error);

            return Ok(result.data);
        }

        [Authorize]
        [HttpPost("Create-new-booking")]
        public async Task<IActionResult> CreateNewBooking([FromBody] CreateBookRequest request)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _bookingService.CreateBookingAsync(request, userId);

            if (!result.success)
                return BadRequest(result.error);

            return Ok(result.data);
        }


        [Authorize(Roles = "Manager, Receptionist")]
        [HttpGet("get-user-booking")]
        public async Task<IActionResult> GetUserBookings()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var bookings = await _bookingService.GetUserBookingsAsync(userId);

            return Ok(bookings);
        }

        [Authorize(Roles = "Manager, Receptionist")]
        [HttpPut("update-booking")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookingRequest request)
        {
            var result = await _bookingService.UpdateBookingAsync(id, request);
            if (!result.success) return BadRequest(result.error);
            return Ok(result.data);
        }

        [Authorize(Roles = "Manager, Receptionist")]
        [HttpDelete("delete-booking")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            await _bookingService.DeleteBookingAsync(id);
            return Ok("ჯავშანი წაიშალა");
        }

        [Authorize]
        [HttpGet("check-availability")]
        public async Task<IActionResult> CheckAvailability(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var isAvailable = await _bookingService.IsRoomAvailableAsync(roomId, checkIn, checkOut);

            return Ok(new { available = isAvailable });
        }
    }
}