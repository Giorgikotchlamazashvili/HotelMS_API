using HotelMS.Application.Interfaces.Repositories;
using HotelMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelMS.Infrastructure.Persistence.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly Baza _baza;

        public HotelRepository(Baza baza)
        {
            _baza = baza;
        }

        public async Task<bool> CountryExistsAsync(string name)
        {
            return await _baza.Countries.AnyAsync(c => c.Name == name);
        }

        public async Task AddCountryAsync(Countries country)
        {
            await _baza.Countries.AddAsync(country);
        }

        public async Task<bool> CountryExistsByIdAsync(int id)
        {
            return await _baza.Countries.AnyAsync(c => c.Id == id);
        }

        public async Task AddCityAsync(City city)
        {
            await _baza.Cities.AddAsync(city);
        }

        public async Task<bool> CityExistsAsync(int id)
        {
            return await _baza.Cities.AnyAsync(c => c.Id == id);
        }

        public async Task AddHotelAsync(Hotels hotel)
        {
            await _baza.Hotels.AddAsync(hotel);
        }

        public async Task<Hotels?> GetHotelByIdAsync(int id)
        {
            return await _baza.Hotels.FindAsync(id);
        }

        public async Task DeleteHotelAsync(Hotels hotel)
        {
            _baza.Hotels.Remove(hotel);
        }

        public async Task<List<Hotels>> GetAllHotelsAsync()
        {
            return await _baza.Hotels
                .Include(h => h.City)
                .Include(h => h.Rooms)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _baza.SaveChangesAsync();
        }
    }
}
