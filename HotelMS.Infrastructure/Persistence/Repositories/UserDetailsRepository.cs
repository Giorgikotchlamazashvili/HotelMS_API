using HotelMS.Domain.Entities;
using HotelMS.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelMS.Infrastructure.Persistence.Repositories
{
    public class UserDetailsRepository : IUserDetailsRepository
    {
        private readonly Baza _baza;

        public UserDetailsRepository(Baza baza)
        {
            _baza = baza;
        }

        public async Task<Users> GetUserWithDetailsByEmailAsync(string email)
        {
            return await _baza.Users
                .Include(u => u.UserDetails)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<UserDetails>> GetAllUserDetailsAsync()
        {
            return await _baza.UserDetails
                .Include(u => u.User)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _baza.SaveChangesAsync();
        }
    }
}