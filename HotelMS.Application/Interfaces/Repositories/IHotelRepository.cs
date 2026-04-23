using HotelMS.Domain.Entities;

namespace HotelMS.Application.Interfaces.Repositories
{
    public interface IHotelRepository
    {
        Task<bool> CountryExistsAsync(string name);
        Task AddCountryAsync(Countries country);

        Task<bool> CountryExistsByIdAsync(int id);
        Task AddCityAsync(City city);

        Task<bool> CityExistsAsync(int id);
        Task AddHotelAsync(Hotels hotel);

        Task<Hotels?> GetHotelByIdAsync(int id);
        Task DeleteHotelAsync(Hotels hotel);

        Task<List<Hotels>> GetAllHotelsAsync();

        Task SaveAsync();
    }
}
