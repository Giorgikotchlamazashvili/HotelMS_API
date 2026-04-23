using HotelMS.Application.DTOs.Response.Payment;

namespace HotelMS.Application.Interfaces.Services;


public interface IPaymentService
{
    Task<(bool success, string error, PaymentResponseDto? data)> CreatePaymentAsync(int bookingId, decimal amount, int paymentMethodId);

    Task<(bool success, string error)> ProcessPaymentAsync(int paymentId);

    Task<PaymentResponseDto?> GetPaymentDetailsAsync(int paymentId);

    Task<PaymentResponseDto?> GetPaymentByBookingIdAsync(int bookingId);
}