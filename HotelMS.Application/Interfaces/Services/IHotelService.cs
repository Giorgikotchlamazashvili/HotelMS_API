

using HotelMS.Application.DTOs.Request.Hotel;
using HotelMS.Application.DTOs.Response.HotelResponse;

namespace HotelMS.Application.Interfaces.Services;
public interface IHotelService
{
    Task<(bool success, string error, int Id)> AddCountryAsync(AddCountryRequest request);
    Task<(bool success, string error, int Id)> AddCityAsync(AddCityRequest request);
    Task<(bool success, string error, AddHotelResponse response)> AddHotelAsync(AddHotelRequest request);
    Task<List<HotelResponse>> GetAllHotelsAsync();
    Task<(bool success, string error)> DeleteHotelAsync(int id);

}
