
using HotelMS.Application.DTOs.Request.Booking;
using HotelMS.Application.DTOs.Response.Booking;
using HotelMS.Domain.Entities;

namespace HotelMS.Application.Interfaces.Services;

public interface IBookingService
{
    Task<IEnumerable<BookingResponse>> GetAllBookingsAsync();
    Task<(bool success, string error, BookingResponse data)> GetBookingByIdAsync(int id);
    Task<(bool success, string error, BookingResponse data)> CreateBookingAsync(CreateBookRequest request, int userId);
    Task<(bool success, string error, BookingResponse data)> UpdateBookingAsync(int id, UpdateBookingRequest request);
    Task DeleteBookingAsync(int id);
    Task<Rooms> GetRoomByIdAsync(int id);
    Task<bool> IsRoomAvailableAsync(int roomId, DateTime checkIn, DateTime checkOut);
    Task<IEnumerable<BookingResponse>> GetUserBookingsAsync(int userId);
}