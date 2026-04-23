using HotelMS.Application.DTOs.Response.PaymentMethod;
using HotelMS.Application.Interfaces.Repositories;

public class PaymentMethodService : IPaymentMethodService
{
    private readonly IPaymentMethodRepository _repository;

    public PaymentMethodService(IPaymentMethodRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<PaymentMethodResponse>> GetPaymentMethodsAsync()
    {
        return await _repository.GetPaymentMethodAsync();
    }
}