using System.Security.Claims;
using HotelMS.Application.DTOs.Request.Invoice;
using HotelMS.Application.DTOs.Response.Invoice;
using HotelMS.Application.Interfaces.Services;
using HotelMS.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;

namespace HotelMS.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public InvoiceService(
        IInvoiceRepository invoiceRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _invoiceRepository = invoiceRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<InvoiceResponse> GetInvoice()
    {
        var userId = _httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");

        }
        var bookings = await _invoiceRepository.GetUserBookingsAsync(int.Parse(userId));
        var bookingList = bookings.ToList();

        if (!bookingList.Any())
        {
            throw new Exception("No bookings found for this user.");
        }
        var latest = bookingList.OrderByDescending(b => b.CreatedAt).First();

        return new InvoiceResponse
        {
            InvoiceNumber = latest.Id.ToString(),
            IssuedAt = latest.CreatedAt,
            InvoicePdfUrl = latest.InvoicePdfUrl,
            Booking = bookingList.Select(b => new InvoiceBookingDto
            {
                BookingId = b.Id,
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                Status = b.Status,
                TotalPrice = b.TotalPrice
            }).ToList()
        };
    }
}