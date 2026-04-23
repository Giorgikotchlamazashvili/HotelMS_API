using HotelMS.Domain.Entities;

namespace HotelMS.Interfaces.Repositories;

public interface IBookingRepository : IInvoiceRepository
{
    Task<IEnumerable<Bookings>> GetAllAsync();
    Task<Bookings> GetByIdAsync(int id);
    Task AddAsync(Bookings booking);
    void Update(Bookings booking);
    void Delete(Bookings booking);
    Task<bool> CheckRoomAvailability(int roomId, DateTime start, DateTime end);
    Task<Rooms> GetRoomByIdAsync(int id);
    public Task SaveChangesAsync();
}
