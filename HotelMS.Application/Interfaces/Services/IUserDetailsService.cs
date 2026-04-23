using HotelMS.Application.DTOs.Request.UserDetailsRequest;
using HotelMS.Application.DTOs.Response.UserDetails;

namespace HotelMS.Application.Interfaces.Services;

public interface IUserDetailsService
{
    Task<(bool success, string error, UserDetailsDto data)> AddUserDetailsAsync(HotelMS.Application.DTOs.Request.UserDetailsRequest.AddUserDetails request);

    Task<(bool success, string error, UserDetailsDto data)> UpdateUserDetailsAsync(UpdateUserDetails request);

    Task<(bool success, string error, IEnumerable<UserDetailsDto> data)> GetAllUserDetailsAsync();
}