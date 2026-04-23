using HotelMS.Domain.Entities;

namespace HotelMS.Interfaces.Repositories
{
    public interface IMainRepositoryUserEmail
    {
        Task<Users> GetUserByEmailAsync(string email);
    }
}
