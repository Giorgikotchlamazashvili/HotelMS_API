using HotelMS.Domain.Entities;

namespace HotelMS.Interfaces.Repositories
{
    public interface IReviewRepository : IMainRepository, IMainRepositoryUserEmail
    {
        Task<Hotels> GetHotelByIdAsync(int id);
        Task<Reviews> GetReviewByIdAsync(int id);
        Task<List<Reviews>> GetReviewsByUserIdAsync(int userId);
        Task<List<Reviews>> GetReviewsByHotelIdAsync(int hotelId);
        Task AddReviewAsync(Reviews review);
        Task RemoveReviewAsync(Reviews review);
    }
}
