using HotelMS.Domain.Entities;

namespace HotelMS.Interfaces.Repositories
{
    public interface IUserRepository : IMainRepository, IMainRepositoryUserEmail
    {
        Task<bool> UserExistsByEmailAsync(string email);
        Task<Users> GetUserByRefreshTokenAsync(string refreshToken);
        Task<List<Users>> GetAllUsersAsync();
        Task AddUserAsync(Users user);
        Task UpdateUserAsync(Users user);
        Task AddNewEmployeeAsync(Users user);
    }
}