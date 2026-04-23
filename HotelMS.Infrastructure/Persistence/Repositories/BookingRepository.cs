using HotelMS.Domain.Entities;
using HotelMS.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelMS.Infrastructure.Persistence.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Baza _baza;

        public BookingRepository(Baza baza)
        {
            _baza = baza;
        }

        public async Task<IEnumerable<Bookings>> GetAllAsync()
        {
            return await _baza.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .ToListAsync();
        }

        public async Task<Bookings> GetByIdAsync(int id)
        {
            return await _baza.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.Id == id);
        }


        public async Task AddAsync(Bookings booking)
        {
            await _baza.Bookings.AddAsync(booking);
        }
        public async Task<Rooms> GetRoomByIdAsync(int id)
        {

            return await _baza.Rooms.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Bookings booking)
        {
            _baza.Bookings.Update(booking);
        }

        public void Delete(Bookings booking)
        {
            _baza.Bookings.Remove(booking);
        }

        public async Task SaveChangesAsync()
        {
            await _baza.SaveChangesAsync();
        }

        public async Task<bool> CheckRoomAvailability(int roomId, DateTime start, DateTime end)
        {

            bool isBooked = await _baza.Bookings.AnyAsync(b =>
                b.RoomId == roomId &&
                ((start >= b.CheckInDate && start < b.CheckOutDate) ||
                 (end > b.CheckInDate && end <= b.CheckOutDate) ||
                 (start <= b.CheckInDate && end >= b.CheckOutDate)));

            return !isBooked;
        }

        public async Task<IEnumerable<Bookings>> GetUserBookingsAsync(int userId)
        {
            return await _baza.Bookings
                .Include(b => b.User)
                .Include(b => b.Room)
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }
    }
}
