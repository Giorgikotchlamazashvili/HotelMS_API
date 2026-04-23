using HotelMS.Domain.Entities;
using HotelMS.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HotelMS.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Baza _baza;

        public UserRepository(Baza baza)
        {
            _baza = baza;
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _baza.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<Users> GetUserByEmailAsync(string email)
        {
            return await _baza.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Users> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _baza.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _baza.Users.ToListAsync();
        }

        public async Task AddUserAsync(Users user)
        {
            await _baza.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _baza.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(Users user)
        {
            _baza.Users.Update(user);
            await _baza.SaveChangesAsync();
        }
        public async Task AddNewEmployeeAsync(Users user)
        {
            await _baza.Users.AddAsync(user);
            await _baza.SaveChangesAsync();
        }
    }
}
