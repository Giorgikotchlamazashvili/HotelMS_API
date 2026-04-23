using HotelMS.Application.DTOs.Response.PaymentMethod;
using HotelMS.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelMS.Infrastructure.Persistence.Repositories
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly Baza _context;

        public PaymentMethodRepository(Baza context)
        {
            _context = context;
        }

        public async Task<List<PaymentMethodResponse>> GetPaymentMethodAsync()
        {
            return await _context.PaymentMethods
                .Select(p => new PaymentMethodResponse
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync();
        }
    }
}
