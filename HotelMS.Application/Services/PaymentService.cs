using AutoMapper;
using HotelMS.Application.DTOs.Response.Payment;
using HotelMS.Application.Interfaces.Services;
using HotelMS.Domain.Entities;
using HotelMS.Interfaces.Repositories;


namespace HotelMS.Application.Services;
public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IBookingRepository _bookingRepository;
    private readonly IMapper _mapper;

    public PaymentService(IPaymentRepository paymentRepository, IBookingRepository bookingRepository, IMapper mapper)
    {
        _paymentRepository = paymentRepository;
        _bookingRepository = bookingRepository;
        _mapper = mapper;
    }

    public async Task<(bool success, string error, PaymentResponseDto? data)> CreatePaymentAsync(int bookingId, decimal amount, int paymentMethodId)
    {
        var booking = await _bookingRepository.GetByIdAsync(bookingId);
        if (booking == null)
            return (false, "ჯავშანი ვერ მოიძებნა.", null);

        var newPayment = new Payment
        {
            BookingId = bookingId,
            Amount = amount,
            PaymentMethodId = paymentMethodId,
            Status = "Pending",
            PaidAt = null
        };

        try
        {
            await _paymentRepository.AddAsync(newPayment);
            await _paymentRepository.SaveChangesAsync();

            var dto = _mapper.Map<PaymentResponseDto>(newPayment);
            return (true, null, dto);
        }
        catch (Exception)
        {
            return (false, "მოხდა გაუთვალისწინებელი შემთხვევა", null);
        }
    }

    public async Task<(bool success, string error)> ProcessPaymentAsync(int paymentId)
    {
        var payment = await _paymentRepository.GetByIdAsync(paymentId);
        if (payment == null)
            return (false, "გადახდა ვერ მოიძებნა.");

        if (payment.Status == "Paid")
            return (false, "ეს გადახდა უკვე შესრულებულია.");

        payment.Status = "Paid";
        payment.PaidAt = DateTime.Now;

        var booking = await _bookingRepository.GetByIdAsync(payment.BookingId);
        if (booking != null)
        {
            booking.Status = "Confirmed";
            _bookingRepository.Update(booking);
            await _bookingRepository.SaveChangesAsync();
        }

        _paymentRepository.Update(payment);
        await _paymentRepository.SaveChangesAsync();

        return (true, null);
    }

    public async Task<PaymentResponseDto?> GetPaymentDetailsAsync(int paymentId)
    {
        var payment = await _paymentRepository.GetByIdAsync(paymentId);
        if (payment == null) return null;

        return _mapper.Map<PaymentResponseDto>(payment);
    }

    public async Task<PaymentResponseDto?> GetPaymentByBookingIdAsync(int bookingId)
    {
        var payment = await _paymentRepository.GetByBookingIdAsync(bookingId);
        if (payment == null) return null;

        return _mapper.Map<PaymentResponseDto>(payment);
    }
}