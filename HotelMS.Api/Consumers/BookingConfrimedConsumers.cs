using HotelMS.Application.DTOs.Request.Invoice;
using HotelMS.Application.DTOs.Response.Invoice;
using HotelMS.Application.Interfaces.Services;
using HotelMS.Domain.Events;
using HotelMS.Infrastructure.ExternalServices;
using HotelMS.Interfaces.Repositories;
using MassTransit;


public class BookingConfirmedConsumer : IConsumer<BookingConfirmedEvent>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IPdfGeneratorService _pdfGeneratorService;
    private readonly S3Services _s3Services;

    public BookingConfirmedConsumer(
        IBookingRepository bookingRepository,
        IPdfGeneratorService pdfGeneratorService,
        S3Services s3Services)
    {
        _bookingRepository = bookingRepository;
        _pdfGeneratorService = pdfGeneratorService;
        _s3Services = s3Services;
    }
    public async Task Consume(ConsumeContext<BookingConfirmedEvent> context)
    {
        var evt = context.Message;

        var booking = await _bookingRepository.GetByIdAsync(evt.BookingId);
        if (booking == null) return;

        var bookings = await _bookingRepository.GetUserBookingsAsync(evt.UserId);

        var invoiceNum = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
        var invoiceResponse = new InvoiceResponse
        {
            InvoiceNumber = invoiceNum,
            IssuedAt = DateTime.UtcNow,
            Booking = bookings.Select(b => new InvoiceBookingDto
            {
                BookingId = b.Id,
                CheckInDate = b.CheckInDate,
                CheckOutDate = b.CheckOutDate,
                Status = b.Status,
                TotalPrice = b.TotalPrice
            }).ToList()
        };


        var pdfBytes = _pdfGeneratorService.GenerateInvoicePdf(invoiceResponse);

        var s3Url = await _s3Services.UploadFileAsync(
            pdfBytes,
            fileName: $"invoice_{invoiceNum}.pdf",
            contentType: "application/pdf",
            folder: $"invoices/user_{evt.UserId}");

        booking.InvoicePdfUrl = s3Url;
        _bookingRepository.Update(booking);
        await _bookingRepository.SaveChangesAsync();
    }
}