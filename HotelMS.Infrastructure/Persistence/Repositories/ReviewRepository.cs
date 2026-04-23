using HotelMS.Domain.Entities;
using HotelMS.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelMS.Infrastructure.Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly Baza _baza;

        public ReviewRepository(Baza baza)
        {
            _baza = baza;
        }
        public async Task<Users> GetUserByEmailAsync(string email)
        {
            return await _baza.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Hotels> GetHotelByIdAsync(int id)
        {
            return await _baza.Hotels.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Reviews> GetReviewByIdAsync(int id)
        {
            return await _baza.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Reviews>> GetReviewsByUserIdAsync(int userId)
        {
            return await _baza.Reviews.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task AddReviewAsync(Reviews review)
        {
            await _baza.Reviews.AddAsync(review);
        }

        public async Task RemoveReviewAsync(Reviews review)
        {
            _baza.Reviews.Remove(review);
        }

        public async Task SaveChangesAsync()
        {
            await _baza.SaveChangesAsync();
        }

        public async Task<List<Reviews>> GetReviewsByHotelIdAsync(int hotelId)
        {
            return await _baza.Reviews
            .Where(r => r.HotelId == hotelId)
            .ToListAsync();
        }
    }
}
