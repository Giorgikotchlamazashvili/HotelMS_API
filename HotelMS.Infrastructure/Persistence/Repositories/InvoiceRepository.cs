using HotelMS.Domain.Entities;
using HotelMS.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelMS.Infrastructure.Persistence.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly Baza _baza;
        public InvoiceRepository(Baza baza)
        {
            _baza = baza;
        }
        public async Task<IEnumerable<Bookings>> GetUserBookingsAsync(int userId)
        {
            var userBookings = await _baza.Bookings.Where(u => u.UserId == userId).ToListAsync();
            return userBookings;
        }
    }
}
