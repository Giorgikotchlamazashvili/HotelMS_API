using HotelMS.Domain.Entities;

namespace HotelMS.Interfaces.Repositories
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Bookings>> GetUserBookingsAsync(int userId);

    }
}
