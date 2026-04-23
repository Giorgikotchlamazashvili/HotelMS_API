using HotelMS.Domain.Entities;

namespace HotelMS.Interfaces.Repositories
{
    public interface IUserDetailsRepository
    {
        Task<Users> GetUserWithDetailsByEmailAsync(string email);
        Task<IEnumerable<UserDetails>> GetAllUserDetailsAsync();
        Task SaveChangesAsync();
    }
}