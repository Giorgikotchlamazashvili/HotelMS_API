using HotelMS.Domain.Entities;
using HotelMS.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelMS.Infrastructure.Persistence.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly Baza _baza;

        public PaymentRepository(Baza baza)
        {
            _baza = baza;
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            return await _baza.Payments.FindAsync(id);
        }

        public async Task<Payment> GetByBookingIdAsync(int bookingId)
        {
            return await _baza.Payments.FirstOrDefaultAsync(p => p.BookingId == bookingId);
        }

        public async Task AddAsync(Payment payment)
        {
            await _baza.Payments.AddAsync(payment);
        }

        public void Update(Payment payment)
        {
            _baza.Payments.Update(payment);
        }

        public async Task SaveChangesAsync()
        {
            await _baza.SaveChangesAsync();
        }
    }

}
