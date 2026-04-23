using HotelMS.Application.DTOs.Response.PaymentMethod;

public interface IPaymentMethodService
{
    Task<IEnumerable<PaymentMethodResponse>> GetPaymentMethodsAsync();
}