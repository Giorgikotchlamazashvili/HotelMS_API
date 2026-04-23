using AutoMapper;
using HotelMS.Application.DTOs.Request.Hotel;
using HotelMS.Application.DTOs.Response.HotelResponse;
using HotelMS.Application.Interfaces.Repositories;
using HotelMS.Application.Interfaces.Services;
using HotelMS.Domain.Entities;
namespace HotelMS.Application.Services

{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<(bool success, string error, int Id)> AddCountryAsync(AddCountryRequest request)
        {
            var exists = await _hotelRepository.CountryExistsAsync(request.Name);

            if (exists)
                return (false, "ეს ქვეყანა უკვე არსებობს.", 0);

            var country = _mapper.Map<Countries>(request);

            await _hotelRepository.AddCountryAsync(country);
            await _hotelRepository.SaveAsync();

            return (true, null, country.Id);
        }

        public async Task<(bool success, string error, int Id)> AddCityAsync(AddCityRequest request)
        {
            var countryExists = await _hotelRepository.CountryExistsByIdAsync(request.CountryId);

            if (!countryExists)
                return (false, "მითითებული ქვეყანა ვერ მოიძებნა.", 0);

            var city = _mapper.Map<City>(request);

            await _hotelRepository.AddCityAsync(city);
            await _hotelRepository.SaveAsync();

            return (true, null, city.Id);
        }

        public async Task<(bool success, string error, AddHotelResponse response)> AddHotelAsync(AddHotelRequest request)
        {
            var cityExists = await _hotelRepository.CityExistsAsync(request.CityId);

            if (!cityExists)
                return (false, "მითითებული ქალაქი ვერ მოიძებნა.", null);

            var hotel = _mapper.Map<Hotels>(request);

            await _hotelRepository.AddHotelAsync(hotel);
            await _hotelRepository.SaveAsync();

            var response = _mapper.Map<AddHotelResponse>(hotel);

            return (true, null, response);
        }

        public async Task<(bool success, string error)> DeleteHotelAsync(int id)
        {
            var hotel = await _hotelRepository.GetHotelByIdAsync(id);

            if (hotel == null)
                return (false, "სასტუმრო მითითებული ID-ით ვერ მოიძებნა.");

            await _hotelRepository.DeleteHotelAsync(hotel);
            await _hotelRepository.SaveAsync();

            return (true, null);
        }

        public async Task<List<HotelResponse>> GetAllHotelsAsync()
        {
            var hotels = await _hotelRepository.GetAllHotelsAsync();

            return _mapper.Map<List<HotelResponse>>(hotels);
        }
    }
}