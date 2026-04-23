using HotelMS.Application.DTOs.Response.PaymentMethod;

namespace HotelMS.Application.Interfaces.Repositories
{
    public interface IPaymentMethodRepository
    {
        Task<List<PaymentMethodResponse>> GetPaymentMethodAsync();
    }
}
